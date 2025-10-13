namespace RegistrationService.Aggregate
{
    public class Area : ValueObject
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Area(int id, string name)
        {
            Id = id;
            Name = name;
        }

        private Area() { }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Name;

        }
    }
}
