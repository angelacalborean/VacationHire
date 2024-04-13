using VacationHire.CurrencyExchangeService;
using Xunit;

namespace VacationHire.UnitTests
{
    public class ExchangeRateRateServiceTests
    {
        ///// <summary>
        /////     This test is commented because I have removed the access key from the code
        ///// </summary>
        ///// <returns></returns>
        //[Fact]
        //public async Task GetExchangeRate_ValidData_ReturnsExchangeRate()
        //{
        //    const string sourceCurrency = "RON";
        //    const string targetCurrency = "EUR";

        //    var httpClient = new HttpClient();

        //    var exchangeService = new CurrencyExchangeService.ExchangeRateRateService(httpClient);
        //    var exchangeRate = await exchangeService.GetExchangeRate(sourceCurrency, targetCurrency);
        //    Assert.NotNull(exchangeRate);
        //    Assert.Equal(sourceCurrency, exchangeRate.SourceCurrency);
        //    Assert.Equal(targetCurrency, exchangeRate.TargetCurrency);
        //    Assert.True(exchangeRate.Rate > (decimal)4.9);
        //}


        [Fact]
        public Task GetExchangeRate_InvalidSourceCurrency_ThrowsException()
        {
            const string sourceCurrency = "InvalidSourceCurrency";
            const string targetCurrency = "EUR";

            var httpClient = new HttpClient();

            var exchangeService = new ExchangeRateRateService(httpClient);
            return Assert.ThrowsAsync<Exception>(async () => await exchangeService.GetExchangeRate(sourceCurrency, targetCurrency));
        }

        [Fact]
        public Task GetExchangeRate_InvalidTargetCurrency_ThrowsException()
        {
            const string sourceCurrency = "RON";
            const string targetCurrency = "InvalidTargetCurrency";

            var httpClient = new HttpClient();

            var exchangeService = new ExchangeRateRateService(httpClient);
            return Assert.ThrowsAsync<Exception>(async () => await exchangeService.GetExchangeRate(sourceCurrency, targetCurrency));
        }
    }
}