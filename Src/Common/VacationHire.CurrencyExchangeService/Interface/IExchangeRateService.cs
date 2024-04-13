using VacationHire.CurrencyExchangeService.BusinessObjects;

namespace VacationHire.CurrencyExchangeService.Interface
{
    /// <summary>
    ///     Interface for the currency exchange service
    /// </summary>
    public interface IExchangeRateService
    {
        /// <summary>
        ///    Get the exchange rate between two currencies
        /// </summary>
        /// <param name="sourceCurrency">Source currency - no validation</param>
        /// <param name="targetCurrency">Target currency - no validation</param>
        /// <returns></returns>
        Task<ExchangeRate> GetExchangeRate(string sourceCurrency, string targetCurrency);
    }
}