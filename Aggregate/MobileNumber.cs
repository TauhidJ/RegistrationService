using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace RegistrationService.Aggregate
{
    public class MobileNumber : ValueObject
    {
        public string Value { get; }

        private static readonly MobileNumber _empty = new(string.Empty);
        public static MobileNumber Empty => _empty;

        private MobileNumber(string value)
        {
            Value = value;
        }

        public static Result<MobileNumber> Create(string? mobileNumber, bool allowNull = false)
        {
            if (allowNull && mobileNumber == null)
                return Result.Success(Empty);
            if (string.IsNullOrWhiteSpace(mobileNumber) || !Regex.IsMatch(mobileNumber, RegexPatterns.MobileNumberWithCountryCode))
                return Result.Failure<MobileNumber>("Mobile number is invalid.");
            return Result.Success(new MobileNumber(mobileNumber));
        }

        public string ValueWithoutCountryCode
        {
            get
            {
                {
                    int countryCodeLength = 0;
                    foreach (var code in Country.List.SelectMany(m => m.CallingCodes))
                    {
                        if (code != null && Value.StartsWith(code, StringComparison.Ordinal) && code.Length > countryCodeLength)
                        {
                            countryCodeLength = code.Length;
                        }
                    }
                    return Value[countryCodeLength..];
                }
            }
        }


        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static implicit operator string(MobileNumber mobileNumber)
        {
            return mobileNumber.Value;
        }
        public static explicit operator MobileNumber(string mobileNo)
        {
            return Create(mobileNo).Value;
        }
    }
}
