using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrationService.Aggregate;
using RegistrationService.Configurations;
using RegistrationService.RequestModel;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RegistrationService.Application.Controllers
{
    [Route("address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private ApplicationDbContext _addressRepository;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public AddressController(ApplicationDbContext addressRepository, IConfiguration configuration)
        {
            _addressRepository = addressRepository;
            _configuration = configuration;
            _httpClient = new HttpClient();
        }


        /// <summary>
        /// Create address for a user.
        /// </summary>
        /// <param name="model">Address details</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync(AddressRequestModel model, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                var apiUrl = $"https://api.postalpincode.in/pincode/{model.PinCode}";
                var response = await _httpClient.GetAsync(apiUrl, cancellationToken);

                if (!response.IsSuccessStatusCode)
                    return BadRequest("Failed to fetch data from Postal API.");

                var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);

                try
                {
                    var jsonNode = JsonNode.Parse(jsonResponse);

                    var postOffice = jsonNode?[0]?["PostOffice"]?[0];

                    if (postOffice == null)
                        return BadRequest("Invalid pin code or no PostOffice data found.");

                    string city = postOffice["District"]?.ToString() ?? "";
                    string state = postOffice["State"]?.ToString() ?? "";

                    var address = new Address(model.UserId!.Value,model.AddressLine!,model.Latitude!.Value,model.Longitude!.Value,model.PinCode!.Value,city,state);

                    await _addressRepository.AddAsync(address);
                    await _addressRepository.SaveChangesAsync(cancellationToken);

                    return Created(string.Empty, address);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error reading postal API response: {ex.Message}");
                }
            }
            return ValidationProblem(ModelState);
        }


        /// <summary>
        /// Return address by id.
        /// </summary>
        /// <param name="id">Address id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpGet("user/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAddressByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var address = await _addressRepository.Address.FindAsync(new object[] { id }, cancellationToken);

            if (address == null) return NotFound($"Address with ID {id} not found.");

            return Ok(address);
        }


        /// <summary>
        /// Return list of address by user id.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAddressByUserIdAsync(long userId, CancellationToken cancellationToken = default)
        {
            var addresses = await _addressRepository.Address.Where(a => a.UserId == userId).ToListAsync();

            if (addresses == null || !addresses.Any()) return NotFound($"No addresses found for user ID {userId}.");

            return Ok(addresses);
        }

    }
}
