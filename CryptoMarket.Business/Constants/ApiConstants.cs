namespace CryptoMarket.Business.Constants
{
    public static class ApiConstants
    {
        public static class Endpoints
        {
            public const string CryptoCurrencyQuotesLatest = "/v2/cryptocurrency/quotes/latest";
        }

        public static class QueryParameters
        {
            public const string Convert = "convert";
            public const string Symbol = "symbol";
        }

        public static class HeaderNames
        {
            public const string ApiKey = "X-CMC_PRO_API_KEY";
            public const string Accept = "Accept";
        }

        public static class HeaderValues
        {
            public const string ApplicationJson = "application/json";
        }
    }
}
