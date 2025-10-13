namespace RegistrationService.Aggregate
{
    public class Gender : Enumeration
    {
        public static readonly Gender Male = new(1, "Male");
        public static readonly Gender Female = new(2, "Female");
        public static readonly Gender Other = new(3, "Other");
        public static readonly Gender PreferNotToDisclose = new(4, "Prefer not to disclose");

        private Gender(int id, string name) : base(id, name) { }
    }
}
