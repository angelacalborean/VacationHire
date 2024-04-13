using System.Text.Json.Serialization;

namespace VacationHire.CurrencyExchangeService.BusinessObjects
{
    /// <summary>
    ///     Map the response from the exchange service
    /// </summary>
    internal class ExternalServiceResponse
    {
        /// <summary>
        ///    The service can be called with multiple currencies, that is why the response looks like this
        /// </summary>
        [JsonPropertyName("data")]
        public Dictionary<string, decimal> Data { get; set; }
    }
}