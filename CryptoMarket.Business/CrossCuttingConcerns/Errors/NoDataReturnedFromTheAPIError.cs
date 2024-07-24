using FluentResults;

namespace CryptoMarket.Business.CrossCuttingConcerns.Errors
{
    public class NoDataReturnedFromTheAPIError : Error
    {
        public NoDataReturnedFromTheAPIError() : base("No data returned from the API")
        {

        }
    }
}
