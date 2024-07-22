using CryptoMarket.Business.Models;
using FluentValidation;

namespace CryptoMarket.Business.Validators
{
    public class CoinMarketCapConfigValidator : AbstractValidator<CoinMarketCapConfig>
    {
        public CoinMarketCapConfigValidator()
        {
            RuleFor(x => x.BaseAddress).NotEmpty();
            RuleFor(x => x.ApiKey).NotEmpty();
            RuleFor(x => x.Currencies).NotNull();
            RuleForEach(x => x.Currencies).NotEmpty();
        }
    }
}
