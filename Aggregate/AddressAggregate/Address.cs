namespace RegistrationService.Aggregate.AddressAggregate
{
    public class Address
    {
        public long Id { get; private set; }
        public long UserId { get; private set; }
        public string AddressLine { get; private set; }
        public decimal Latitude { get; private set; }
        public decimal Longitude { get; private set; }
        public long PinCode { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }

        public Address(long userId, string addressLine, decimal latitude, decimal longitude, long pinCode, string city, string state)
        {
            UserId = userId;
            AddressLine = addressLine;
            Latitude = latitude;
            Longitude = longitude;
            PinCode = pinCode;
            City = city;
            State = state;
        }
    }
}
