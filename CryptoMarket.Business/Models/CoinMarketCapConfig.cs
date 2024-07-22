namespace CryptoMarket.Business.Models
{
    public class CoinMarketCapConfig
    {
        public string BaseAddress { get; set; }
        public string ApiKey { get; set; }
        public string[] Currencies { get; set; }
    }
}
