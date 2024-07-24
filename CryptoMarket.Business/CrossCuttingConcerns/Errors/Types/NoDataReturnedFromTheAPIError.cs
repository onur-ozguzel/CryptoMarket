using FluentResults;

namespace CryptoMarket.Business.CrossCuttingConcerns.Errors.Types
{
    public class NoDataReturnedFromTheAPIError : Error
    {
        public NoDataReturnedFromTheAPIError() : base("No data returned from the API")
        {

        }
    }
}
