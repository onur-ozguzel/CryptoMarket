using CryptoMarket.Business.CrossCuttingConcerns.Errors;
using CryptoMarket.Business.Services;
using CryptoMarket.WebAPI.Authorization;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoMarket.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlertController : ControllerBase
    {
        private readonly ICoinMarketCapService _coinMarketCapService;
        private readonly IErrorHandlingService _errorHandlingService;
        public AlertController(ICoinMarketCapService coinMarketCapService, IErrorHandlingService errorHandlingService)
        {
            _coinMarketCapService = coinMarketCapService;
            _errorHandlingService = errorHandlingService;
        }

        [HttpPost]
        [Authorize(Policy = "ClientApplicationCanWrite")]
        public async Task<IActionResult> CreateAlertAsync(CancellationToken cancellationToken, string alertName)
        {
            // CreateAlert

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "MustOwnAlert")]
        public async Task<IActionResult> DeleteAlertAsync(CancellationToken cancellationToken, string id)
        {
            // DeleteAlert

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("{id}")]
        [MustOwnAlert]
        public async Task<IActionResult> UpdateAlertAsync(CancellationToken cancellationToken, string id, string name)
        {
            // DeleteAlert

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
