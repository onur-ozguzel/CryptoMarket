using FluentResults;

namespace CryptoMarket.Business.CrossCuttingConcerns.Errors
{
    public class ArgumentNullOrEmptyError : Error
    {
        public ArgumentNullOrEmptyError(string parameterName) : base($"Parameter {parameterName} cannot be null or empty")
        {
        }
    }
}
