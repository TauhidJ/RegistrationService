namespace RegistrationService.Aggregate
{
    public class ResultSuccessException : Exception
    {
        internal ResultSuccessException()
        : base(Result.Messages.ErrorIsInaccessibleForSuccess)
        {
        }
    }
}
