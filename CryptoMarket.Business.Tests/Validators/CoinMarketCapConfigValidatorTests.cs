using CryptoMarket.Business.Models;
using CryptoMarket.Business.Validators;
using FluentValidation.TestHelper;

namespace CryptoMarket.Business.Tests.Validators
{
    public class CoinMarketCapConfigValidatorTests
    {
        private readonly CoinMarketCapConfigValidator _validator;

        public CoinMarketCapConfigValidatorTests()
        {
            _validator = new CoinMarketCapConfigValidator();
        }

        [Fact]
        public void Validate_BaseAddressIsEmpty_ShouldHaveValidationError()
        {
            var model = new CoinMarketCapConfig { BaseAddress = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.BaseAddress);
        }

        [Fact]
        public void Validate_BaseAddressIsSpecified_ShouldNotHaveValidationError()
        {
            var model = new CoinMarketCapConfig { BaseAddress = "https://api.coinmarketcap.com" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.BaseAddress);
        }

        [Fact]
        public void Validate_ApiKeyIsEmpty_ShouldHaveValidationError()
        {
            var model = new CoinMarketCapConfig { ApiKey = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.ApiKey);
        }

        [Fact]
        public void Validate_ApiKeyIsSpecified_ShouldNotHaveValidationError()
        {
            var model = new CoinMarketCapConfig { ApiKey = "some-api-key" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.ApiKey);
        }

        [Fact]
        public void Validate_CurrenciesIsNull_ShouldHaveValidationError()
        {
            var model = new CoinMarketCapConfig { Currencies = null };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Currencies);
        }

        [Theory]
        [InlineData([new string[] { "BTC", "" }])]
        [InlineData([new string[] { "", "BTC" }])]
        [InlineData([new string[] { " ", "BTC" }])]
        [InlineData([new string[] { "", "  " }])]
        [InlineData([new string[] { "BTC", null }])]
        public void Validate_CurrenciesContainsInvalidValue_ShouldHaveValidationError(string[] currencies)
        {
            var model = new CoinMarketCapConfig { Currencies = currencies };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Currencies);
        }

        [Fact]
        public void Validate_CurrenciesIsSpecifiedAndValid_ShouldNotHaveValidationError()
        {
            var model = new CoinMarketCapConfig { Currencies = ["BTC", "ETH"] };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Currencies);
        }
    }
}
