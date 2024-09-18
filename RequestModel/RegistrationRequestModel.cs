using System.ComponentModel.DataAnnotations;

namespace RegistrationService.RequestModel
{
    public class RegistrationRequestModel
    {
        [Required(ErrorMessage = "First name is required.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }


        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password, ErrorMessage = "Invalid password format.")]
        public string? Password { get; set; }

    }
}
