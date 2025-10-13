using System.Net.Mail;
using System.Reflection;
using System.Xml.Linq;

namespace RegistrationService.Aggregate
{
    public class User
    {
        public long Id { get; private set; }
        public long? ReferrerUserId { get; private set; }
        public Name Name { get; private set; }
        public MobileNumber? MobileNumber { get; private set; }
        public EmailAddress? EmailAddress { get; private set; }
        public string? MobileNumberWithoutCountryCode { get; private set; }
        public string? NormalizedEmailAddress { get; private set; }
        public bool IsEmailAddressVerified { get; private set; } = false;
        public bool IsMobileNumberVerified { get; private set; } = false;

        string? _passwordHash;

        public DateTime? LastChangedPasswordDateTime { get; private set; }
        public string Locale { get; private set; } = "en-IN";

        public DateTime LastUpdatedDateTime { get; private set; }

        public DateTime RegistrationDateTime { get; private set; }

        public string? PictureUrl { get; private set; }

        //private List<UserLogin> _logins = new();
        //public IReadOnlyList<UserLogin> Logins => _logins.AsReadOnly();

        //private List<UserFCMToken> _fcmTokens = new();
        //public IReadOnlyList<UserFCMToken> FcmTokens => _fcmTokens.AsReadOnly();

        public bool HasPassword => _passwordHash != null;

        public DateOfBirth? DateOfBirth { get; private set; }
        public int? GenderId { get; private set; }
        //public Gender Gender => Enumeration.FromValue<Gender>((int)GenderId);
        public Location? Location { get; private set; }
        public decimal? AverageRating { get; private set; }
        public int? RatingCount { get; private set; }
        public UpdaterUser? Creator { get; private set; }
        public Guid? RegistrationClientAppId { get; private set; }
        private User() { }
    }
}
