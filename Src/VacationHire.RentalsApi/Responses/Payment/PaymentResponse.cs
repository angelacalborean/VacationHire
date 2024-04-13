using Newtonsoft.Json;

namespace VacationHire.RentalsApi.Responses.Payment
{
    /// <summary>
    ///     Payment response object: it can be extended with additional properties (to track the payment for example)
    /// </summary>
    public class PaymentResponse
    {
        /// <summary>
        ///     If everything was ok, confirm the payment
        /// </summary>
        public int ConfirmationId { get; set; }

        public decimal SourceAmount { get; set; }

        public string SourceCurrency { get; set; }

        public decimal TargetAmount { get; set; }

        public string TargetCurrency { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}