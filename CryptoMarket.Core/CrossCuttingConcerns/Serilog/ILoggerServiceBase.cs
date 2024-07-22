namespace CryptoMarket.Core.CrossCuttingConcerns.Serilog
{
    public interface ILoggerServiceBase
    {
        void Debug(string message);
        void Error(string message);
        void Fatal(string message);
        void Info(string message);
        void Verbose(string message);
        void Warn(string message);
    }
}