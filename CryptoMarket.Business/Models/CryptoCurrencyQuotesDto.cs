namespace CryptoMarket.Business.Models
{
    public class CryptoCurrencyQuotesDto
    {
        public string? Name { get; set; }
        public string? Symbol { get; set; }
        public string? Slug { get; set; }
        public List<KeyValuePair<string, decimal?>>? Currencies { get; set; }

    }
}
