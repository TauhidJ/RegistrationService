namespace RegistrationService.Aggregate
{
    public class Name : ValueObject
    {
        public string FirstName { get; }

        public string? LastName { get; }

        private Name(string firstName, string? lastName = null)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static readonly char[] _notAllowedCharacters = new char[] { '$', '^', '`', '<', '>', '+', '/', '=', '~' };

        public static Result<Name> Create(string firstName, string? lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName)) { return Result.Failure<Name>("First name can't be blank."); }

            var combinedString = firstName + lastName;
            if (combinedString.Length > 100) return Result.Failure<Name>("Name is too long.");
            if (combinedString.IndexOfAny(_notAllowedCharacters) > -1) return Result.Failure<Name>("Some special characters are not allowed in the name.");

            return Result.Success(new Name(firstName, lastName));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;

            if (LastName != null) yield return LastName;
        }

        public static implicit operator string(Name name)
        {
            return name.FirstName + " " + name.LastName;
        }

        //public static explicit operator Name(string name)
        //{
        //    return Create(name).Value;
        //}
    }
}
