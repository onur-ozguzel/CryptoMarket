namespace CryptoMarket.Business.Constants
{
    public static class ErrorMessages
    {
        public const string NoDataReturnedFromTheAPI = "No data returned from the API";
        public const string FailedToFetchData = "Failed to fetch data from 3rd party API. Status code: {0}";
        public const string ParameterCannotBeNullOrEmpty = "Parameter '{0}' cannot be null or empty.";
        public const string CoinMarketCapConfigurationIsMissingOrInvalid = "CoinMarketCap configuration is missing or invalid.";
        public const string AnUnexpectedErrorOccurred = "An unexpected error occurred.";
    }
}
