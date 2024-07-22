namespace CryptoMarket.Business.Models.Responses.GetCryptoCurrencyQuotes
{
    public class CryptocurrencyData
    {
        public string? Name { get; set; }

        public string? Symbol { get; set; }

        public string? Slug { get; set; }

        public Dictionary<string, Quote>? Quote { get; set; }
    }
}
