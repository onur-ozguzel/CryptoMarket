using FluentResults;

namespace CryptoMarket.Business.CrossCuttingConcerns.Errors
{
    public class UnauthorizedError : Error
    {
        public UnauthorizedError() : base("Unauthorized. Access is denied due to invalid credentials.")
        {

        }
    }
}
