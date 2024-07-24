using CryptoMarket.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace CryptoMarket.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoQuotesController : ControllerBase
    {
        private readonly ICoinMarketCapService _coinMarketCapService;
        private readonly IErrorHandlingService _errorHandlingService;

        public CryptoQuotesController(IErrorHandlingService errorHandlingService, ICoinMarketCapService coinMarketCapService)
        {
            _coinMarketCapService = coinMarketCapService;
            _errorHandlingService = errorHandlingService;
        }

        /// <summary>
        /// Gets the latest cryptocurrency quotes.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <param name="symbol">Cryptocurrency symbol.</param>
        /// <returns>ActionResult with the cryptocurrency quotes.</returns>
        [HttpGet]
        public async Task<IActionResult> GetCryptoCurrencyQuotesAsync(CancellationToken cancellationToken, string symbol)
        {
            var result = await _coinMarketCapService.GetCryptoCurrencyQuotesAsync(cancellationToken, symbol);

            return _errorHandlingService.HandleResult(result);
        }
    }
}
