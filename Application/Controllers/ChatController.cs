using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace RegistrationService.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public ChatController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public class ChatRequest
        {
            public string Message { get; set; }
        }


        /// <summary>
        /// Handles chat requests by forwarding user messages to the Hugging Face API.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Chat([FromBody] ChatRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Message))
                return BadRequest("Message cannot be empty.");

            var token = _config["HuggingFace:ApiToken"];
            if (string.IsNullOrWhiteSpace(token))
                return BadRequest("Hugging Face token missing in configuration.");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var body = new
            {
                model = "Kwaipilot/KAT-Dev:novita",
                messages = new[]
                {
                    new { role = "system", content = "You are an AI assistant. Always reply in English." },
                    new { role = "user", content = request.Message }
                }
            };

            var response = await client.PostAsJsonAsync(
                "https://router.huggingface.co/v1/chat/completions",
                body
            );

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, error);
            }

            var result = await response.Content.ReadFromJsonAsync<JsonElement>();

            // Extract assistant reply
            var assistantReply = result
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return Ok(new { reply = assistantReply });
        }
    }


}
//
// https://huggingface.co/
// Pass = TauhidJafri

