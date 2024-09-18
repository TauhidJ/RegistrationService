using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrationService.Aggregate;
using RegistrationService.Configurations;
using RegistrationService.RequestModel;

namespace RegistrationService.Application.Controllers
{
    [Route("registration")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private ApplicationDbContext _registrationRepository;

        public RegistrationController(ApplicationDbContext studentRepository)
        {
            _registrationRepository = studentRepository;
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

                return CreatedAtAction(nameof(GetStudentById), new { id = st.Id }, st);


            }
            return ValidationProblem(ModelState);
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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

            return Ok("User verified successfully.");
        }






    }
}
