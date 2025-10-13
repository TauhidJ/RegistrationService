using Microsoft.AspNetCore.Identity;

namespace RegistrationService.Aggregate
{
    public class DateOfBirth : ValueObject
    {
        public DateTime? Value { get; private set; }

        private DateOfBirth(DateTime value)
        {
            Value = value;
        }
        /// <summary>
        /// Create date of birth
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Date of birth created.</returns>
        public static Result<DateOfBirth> Create(DateTime value)
        {
            DateTime today = DateTime.UtcNow.Date;
            DateTime minDate = today.AddYears(-100);

            if (value == today || value < minDate)
            {
                return Result.Failure<DateOfBirth>(new InvalidDateOfBirthError("Date of birth should not be today's date and not earlier than 100 years ago."));
            }

            return Result.Success(new DateOfBirth(value));
        }


        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static explicit operator DateTime?(DateOfBirth dateOfBirth)
        {
            return dateOfBirth?.Value;
        }


        public class InvalidDateOfBirthError : Error
        {
            public InvalidDateOfBirthError(string message) : base(message) { }
        }
    }
}
