namespace RegistrationService.Aggregate
{
    public class UpdaterUser : ValueObject
    {
        public long Id { get; private set; }
        public string Name { get; private set; }

        private UpdaterUser() { }

        public UpdaterUser(long id, string name)
        {
            Id = id;
            Name = name;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Name;
        }
    }
}
