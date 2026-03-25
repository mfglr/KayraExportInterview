namespace LogService.Domain
{
    public class Exception(string message, string stackTrace, Exception? innerException = null)
    {
        public string Message { get; private set; } = message;
        public string StackTrace { get; private set; } = stackTrace;
        public Exception? InnerException { get; private set; } = innerException;
    }
}
