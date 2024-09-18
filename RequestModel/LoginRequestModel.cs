using System.ComponentModel.DataAnnotations;

namespace RegistrationService.RequestModel
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password, ErrorMessage = "Invalid password format.")]
        public string? Password { get; set; }
    }
}
