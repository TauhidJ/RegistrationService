namespace RegistrationService.Aggregate
{
    public interface IValue<out T>
    {
        T Value { get; }
    }
}
