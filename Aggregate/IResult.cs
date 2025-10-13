namespace RegistrationService.Aggregate
{
    public interface IResult
    {
        bool IsFailure { get; }

        bool IsSuccess { get; }
    }
}
