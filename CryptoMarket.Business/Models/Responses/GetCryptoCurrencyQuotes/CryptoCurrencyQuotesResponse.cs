namespace CryptoMarket.Business.Models.Responses.GetCryptoCurrencyQuotes
{
    public class CryptoCurrencyQuotesResponse
    {
        public Dictionary<string, List<CryptocurrencyData>>? Data { get; set; }

        /// <summary>
        /// TODO: To be extracted if it is a common property for other responses
        /// </summary>
        public Status? Status { get; set; }
    }
}
