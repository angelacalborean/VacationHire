using System.ComponentModel.DataAnnotations;

namespace VacationHire.RentalsApi.Requests.Payment
{
    /// <summary>
    ///     Payment request object: it can be extended with additional properties
    /// </summary>
    public class PaymentRequest
    {
        /// <summary>
        ///     Amount to be paid in source currency
        /// </summary>
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal Amount { get; set; }

        /// <summary>
        ///    Source currency
        /// </summary>
        [Required]
        [StringLength(3)]
        public string SourceCurrency { get; set; }

        /// <summary>
        ///     Target currency to which the amount would be converted
        /// </summary>
        [Required]
        [StringLength(3)]
        public string TargetCurrency { get; set; }
    }
}