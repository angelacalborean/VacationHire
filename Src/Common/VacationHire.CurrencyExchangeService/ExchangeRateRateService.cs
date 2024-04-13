using System.Text.Json;
using VacationHire.CurrencyExchangeService.BusinessObjects;
using VacationHire.CurrencyExchangeService.Interface;

namespace VacationHire.CurrencyExchangeService
{
    public class ExchangeRateRateService : IExchangeRateService
    {
        private readonly HttpClient _httpClient;
        public const string BaseUrl = "https://api.freecurrencyapi.com/v1/";

        private const string ApiAccessKey = "FILL ME IN";

        /// <summary>
        ///     Default http client is injected in the ctor, policies can be added to the client
        /// </summary>
        /// <param name="httpClient"></param>
        public ExchangeRateRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        ///     The api has also methods to validate the currencies; no validation is added here
        /// </summary>
        /// <param name="sourceCurrency">Tried it out with RON/EUR/USD/NOK</param>
        /// <param name="targetCurrency"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ExchangeRate> GetExchangeRate(string sourceCurrency, string targetCurrency)
        {
            // I have not saved the api key, in order to make this work, you need to add your own api key

            // If you do not have an api key, use the commented code below to get a dummy response
            //return new ExchangeRate(sourceCurrency, targetCurrency, (decimal)4.3, DateTime.UtcNow);

            var completeUrl = BaseUrl + "latest?currencies=" + sourceCurrency + "&base_currency=" + targetCurrency + "&apikey=" + ApiAccessKey;
            var response = await _httpClient.GetAsync(completeUrl);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error calling CurrencyService API: {response.StatusCode}");

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<ExternalServiceResponse>(content);

            if (data == null)
                throw new Exception("Error deserializing response from CurrencyService API");

            return new ExchangeRate(sourceCurrency, targetCurrency, data.Data[sourceCurrency], DateTime.UtcNow);
        }
    }
}