using CryptoMarket.Business.CrossCuttingConcerns.Errors.Types;
using CryptoMarket.Business.Models;
using CryptoMarket.Business.Models.Responses.GetCryptoCurrencyQuotes;
using FluentResults;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;
using static CryptoMarket.Business.Constants.ApiConstants;

namespace CryptoMarket.Business.Services
{
    public class CoinMarketCapService : ICoinMarketCapService
    {
        private readonly HttpClient _httpClient;
        private readonly CoinMarketCapConfig _coinMarketCapConfig;

        public CoinMarketCapService(HttpClient httpClient, CoinMarketCapConfig coinMarketCapConfig)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _coinMarketCapConfig = coinMarketCapConfig ?? throw new ArgumentNullException(nameof(coinMarketCapConfig));

            ConfigureHttpClient();
        }

        public async Task<Result<List<CryptoCurrencyQuotesDto>>> GetCryptoCurrencyQuotesAsync(CancellationToken cancellationToken, string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                return Result.Fail(new ArgumentNullOrEmptyError(nameof(symbol)));
            }

            var url = GetCryptoCurrencyQuotesBuildUrl(symbol);
            var httpResponseMessage = await _httpClient.GetAsync(url, cancellationToken);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                return Result.Fail(new FailedToFetchDataError((int)httpResponseMessage.StatusCode));
            }

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };

            var apiResult = await httpResponseMessage.Content.ReadFromJsonAsync<CryptoCurrencyQuotesResponse>(serializeOptions, cancellationToken);

            if (apiResult == null || apiResult.Data == null || !apiResult.Data.Any() || !apiResult.Data.Single().Value.Any())
            {
                return Result.Fail(new NoDataReturnedFromTheAPIError());
            }

            var results = MapGetCryptoCurrencyQuotesResponse(apiResult.Data.Values.Single());

            return Result.Ok(results);
        }

        private List<CryptoCurrencyQuotesDto> MapGetCryptoCurrencyQuotesResponse(List<CryptocurrencyData> cryptocurrencyData)
        {
            return cryptocurrencyData
                .Select(MapCryptocurrencyDataToDto)
                .ToList();
        }

        private CryptoCurrencyQuotesDto MapCryptocurrencyDataToDto(CryptocurrencyData cryptocurrencyData)
        {
            return new CryptoCurrencyQuotesDto
            {
                Name = cryptocurrencyData.Name,
                Symbol = cryptocurrencyData.Symbol,
                Slug = cryptocurrencyData.Slug,
                Currencies = MapQuotesToCurrencies(cryptocurrencyData.Quote)
            };
        }

        private List<KeyValuePair<string, decimal?>> MapQuotesToCurrencies(Dictionary<string, Quote> quotes)
        {
            return quotes
                .Select(quote => new KeyValuePair<string, decimal?>(quote.Key, quote.Value.Price))
                .ToList();
        }

        private void ConfigureHttpClient()
        {
            _httpClient.BaseAddress = new Uri(_coinMarketCapConfig.BaseAddress);
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, HeaderValues.ApplicationJson);
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.ApiKey, _coinMarketCapConfig.ApiKey);
        }

        private string GetCryptoCurrencyQuotesBuildUrl(string symbol)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString[QueryParameters.Convert] = string.Join(",", _coinMarketCapConfig.Currencies);
            queryString[QueryParameters.Symbol] = symbol;

            var path = Endpoints.CryptoCurrencyQuotesLatest;
            return $"{path}?{queryString}";
        }
    }
}
