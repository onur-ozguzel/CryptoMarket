namespace CryptoMarket.Core.CrossCuttingConcerns.Logging
{
    public class LogDetailWithException : LogDetail
    {
        public string ExceptionMessage { get; set; }

        public LogDetailWithException()
        {
            ExceptionMessage = string.Empty;
        }

        public LogDetailWithException(string exceptionMessage, string fullName, string methodName, string user, List<LogParameter> parameters)
            : base(fullName, methodName, user, parameters)
        {
            ExceptionMessage = exceptionMessage;
        }
    }
}
