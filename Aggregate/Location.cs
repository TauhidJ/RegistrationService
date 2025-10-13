using Microsoft.AspNetCore.Identity;

namespace RegistrationService.Aggregate
{
    public class Location : ValueObject
    {
        public long? AddressId { get; private set; }
        public decimal Latitude { get; private set; }
        public decimal Longitude { get; private set; }
        public Area? Area { get; private set; }

        private Location(long? id, decimal latitude, decimal longitude, Area? area)
        {
            AddressId = id;
            Latitude = latitude;
            Longitude = longitude;
            Area = area;
        }
        private Location() { }

        /// <summary>
        /// Create location.
        /// </summary>
        /// <param name="id">Address id</param>
        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        /// <param name="area">Area</param>
        /// <returns>
        /// <para>
        /// <list type="bullet">
        /// <item><term>Success value</term><description>If the location created.</description></item>
        /// <item><term>Failure errors:</term><description>
        /// <list type="table">
        /// <item><term><see cref="InvalidLocationCoordinatesError"/></term><description>If the location coordinates are invalid.</description></item>
        /// </list>
        /// </description></item>
        /// </list>
        /// </para>
        /// </returns>
        public static Result<Location> Create(long? id, decimal latitude, decimal longitude, Area? area)
        {
            if (latitude < -90 || latitude > 90) return Result.Failure<Location>(new InvalidLocationCoordinatesError("Latitude value is out of range."));
            if (longitude < -180 || longitude > 180) return Result.Failure<Location>(new InvalidLocationCoordinatesError("Longitude value is out of range."));
            return Result.Success(new Location(id, latitude, longitude, area));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return AddressId;
            yield return Latitude;
            yield return Longitude;
            yield return Area;
        }


        public class InvalidLocationCoordinatesError : Error
        {
            public InvalidLocationCoordinatesError(string message) : base(message) { }
        }
    }
}
