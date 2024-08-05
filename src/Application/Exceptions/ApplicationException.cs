namespace Application.Exceptions
{
    public class ApplicationException : Exception
    {
        protected ApplicationException(string? messageEx) : base(messageEx) { }
    }
}
