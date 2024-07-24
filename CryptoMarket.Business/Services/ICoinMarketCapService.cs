using CryptoMarket.Business.Models;
using FluentResults;

namespace CryptoMarket.Business.Services
{
    public interface ICoinMarketCapService
    {
        Task<Result<List<CryptoCurrencyQuotesDto>>> GetCryptoCurrencyQuotesAsync(CancellationToken cancellationToken, string symbol);
    }
}