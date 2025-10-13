using System.ComponentModel.DataAnnotations;

namespace RegistrationService.RequestModel
{
    public class AddressRequestModel
    {
        [Required(ErrorMessage = "User ID is required.")]
        public long? UserId { get; set; }

        [Required(ErrorMessage = "Address Line is required.")]
        public string AddressLine { get; set; }

        [Required(ErrorMessage = "Latitude is required.")]
        public decimal? Latitude { get; set; }

        [Required(ErrorMessage = "Longitude is required.")]
        public decimal? Longitude { get; set; }

        [Required(ErrorMessage = "Pincode is required.")]
        public long? PinCode { get; set; }
    }
}
