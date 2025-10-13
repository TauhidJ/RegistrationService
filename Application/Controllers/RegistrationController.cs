using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RegistrationService.Aggregate.RegistrationAggregate;
using RegistrationService.Configurations;
using RegistrationService.RequestModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RegistrationService.Application.Controllers
{
    [Route("registration")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private ApplicationDbContext _registrationRepository;
        private readonly IConfiguration _configuration;

        public RegistrationController(ApplicationDbContext studentRepository, IConfiguration configuration)
        {
            _registrationRepository = studentRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("/register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync(RegistrationRequestModel model, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                var st = new Registration(model.FirstName!, model.LastName!, model.Email!, model.PhoneNumber!, model.Password);

                await _registrationRepository.AddAsync(st);
                await _registrationRepository.SaveChangesAsync(cancellationToken);

                // Now generate the JWT token
                var claims = new[] {
            
                    new Claim(JwtRegisteredClaimNames.Sub, model.Email!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("UserId", st.Id.ToString()),
                    new Claim("DisplayName", model.FirstName!),
                    new Claim("Email", model.Email!)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn
                );
                return CreatedAtAction(nameof(GetStudentById), new { id = st.Id }, new
                {
                    Student = st,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            return ValidationProblem(ModelState);
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("/get-by-id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudentById(int id, CancellationToken cancellationToken)
        {
            var student = await _registrationRepository.Registration.FindAsync(new object[] { id }, cancellationToken);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        /// <summary>
        /// Verify user by email.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
       
        [HttpPost("/verify")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> VerifyStudentAsync(LoginRequestModel model, CancellationToken cancellationToken = default)
        {
            var student = await _registrationRepository.Registration
                .FirstOrDefaultAsync(s => s.Email == model.Email && s.Password == model.Password, cancellationToken);

            if (student == null)
            {
                return Unauthorized("Invalid Email or Password.");
            }
            return Ok("User verified successfully." );
        }
    }
}
