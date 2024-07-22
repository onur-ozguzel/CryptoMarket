using CryptoMarket.Business.Models;

namespace CryptoMarket.Business.Services
{
    public interface ICoinMarketCapService
    {
        Task<List<CryptoCurrencyQuotesDto>> GetCryptoCurrencyQuotesAsync(CancellationToken cancellationToken, string symbol);
    }
}