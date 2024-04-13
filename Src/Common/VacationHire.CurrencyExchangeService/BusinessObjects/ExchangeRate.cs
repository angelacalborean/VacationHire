namespace VacationHire.CurrencyExchangeService.BusinessObjects
{
    /// <summary>
    ///     Exchange rate between two currencies: as an improvement:
    ///     - add enums of accepted currencies (here there is no validation, some currencies might not exist)
    ///     - add max amount for certain currencies
    /// </summary>
    public class ExchangeRate(string sourceCurrency, string targetCurrency, decimal rate, DateTime exchangeDateTimeUtc)
    {
        /// <summary>
        ///     Might be redundant, but it's good to have it
        /// </summary>
        public string SourceCurrency { get; set; } = sourceCurrency;

        /// <summary>
        ///     Same here
        /// </summary>
        public string TargetCurrency { get; set; } = targetCurrency;

        /// <summary>
        ///     Exchange rate between the two currencies: source and target
        /// </summary>
        public decimal Rate { get; set; } = rate;

        /// <summary>
        ///     Date and time of the exchange rate since it differs if called at different times
        /// </summary>
        public DateTime ExchangeDateTimeUtc { get; set; } = exchangeDateTimeUtc;
    }
}