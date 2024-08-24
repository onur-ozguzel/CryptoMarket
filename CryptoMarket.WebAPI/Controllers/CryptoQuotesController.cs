using CryptoMarket.Business.CrossCuttingConcerns.Errors;
using CryptoMarket.Business.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoMarket.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
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
        [Authorize(Policy = "PremiumUser")]
        public async Task<IActionResult> GetCryptoCurrencyQuotesPremiumAsync(CancellationToken cancellationToken, string symbol)
        {
            var ownerId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            if (ownerId == null)
            {
                return _errorHandlingService.HandleError(Result.Fail(new UnauthorizedError()));
            }

            var result = await _coinMarketCapService.GetCryptoCurrencyQuotesAsync(cancellationToken, symbol);

            return _errorHandlingService.HandleResult(result);
        }

        [HttpGet]
        //[Authorize(Roles = "PayingUser, FreeUser")]
        public async Task<IActionResult> GetCryptoCurrencyQuotesNormalAsync(CancellationToken cancellationToken, string symbol)
        {
            var ownerId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            if (ownerId == null)
            {
                return _errorHandlingService.HandleError(Result.Fail(new UnauthorizedError()));
            }

            var result = await _coinMarketCapService.GetCryptoCurrencyQuotesAsync(cancellationToken, symbol);

            return _errorHandlingService.HandleResult(result);
        }
    }
}
