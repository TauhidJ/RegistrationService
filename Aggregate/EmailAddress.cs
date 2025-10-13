using System.Text.RegularExpressions;

namespace RegistrationService.Aggregate
{
    public class EmailAddress : ValueObject
    {
        public string Value { get; }

        public string NormalizedValue => Value.ToUpperInvariant();

        private static readonly EmailAddress _empty = new(string.Empty);
        public static EmailAddress Empty => _empty;

        private EmailAddress(string value)
        {
            Value = value;
        }

        public static Result<EmailAddress> Create(string? emailAddress, bool allowNull = false)
        {
            if (allowNull && emailAddress == null)
                return Result.Success(Empty);
            if (string.IsNullOrWhiteSpace(emailAddress) || !Regex.IsMatch(emailAddress, RegexPatterns.EmailAddress))
                return Result.Failure<EmailAddress>("Email address is invalid.");
            return Result.Success(new EmailAddress(emailAddress));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static implicit operator string(EmailAddress emailAddress)
        {
            return emailAddress.Value;
        }
        public static explicit operator EmailAddress(string emailAddress)
        {
            return Create(emailAddress).Value;
        }
    }
}
