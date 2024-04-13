using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacationHire.Common;
using VacationHire.Common.GenericResponses;
using VacationHire.CurrencyExchangeService.Interface;
using VacationHire.RentalsApi.Requests.Payment;
using VacationHire.RentalsApi.Responses.Payment;

namespace VacationHire.RentalsApi.Controllers
{
    /// <summary>
    ///     Controller that deals with payments
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : BaseController
    {
        private readonly ILogger<PaymentController> _logger;

        private readonly IExchangeRateService _exchangeRateService;

        public PaymentController(IExchangeRateService exchangeRateService, ILogger<PaymentController> logger)
        {
            _logger = logger;

            _exchangeRateService = exchangeRateService;
        }

        /// <summary>
        ///     Allows a user to rent a car
        /// - Authentication is required
        /// - Authorization: checks needed to ensure the user has the needed permissions to rent a car (demo purposes: ask for User role)
        ///
        /// - No validation is added here, but it should be added in a real-world scenario
        ///     - have an [FromBody] RentRequest request with more details about the rental
        /// </summary>
        /// <param name="invoiceId">Invoice identifier</param>
        /// <param name="paymentRequest">Details about the payment</param>
        /// <returns></returns>
        [HttpPost("{invoiceId}/pay")]
        [Authorize(Roles = "User")] // User role is required to rent a car
        [ProducesResponseType(typeof(PaymentResponse), 200)]
        [ProducesResponseType(typeof(ApiBadRequestResult), 400)]
        [ProducesResponseType(typeof(ApiNotFoundResult), 404)]
        [ProducesResponseType(typeof(ApiInternalErrorResult), 500)]
        public async Task<IActionResult> PayInvoice(int invoiceId, [FromBody] PaymentRequest paymentRequest)
        {
            try
            {
                // Improvements:
                //      - Validate the payment request;
                //      - check that currencies are valid id user is allowed to enter free text for the currency; escape them; better approach use an enum; or specific values;
                //      - check that the amount is a positive number;
                //      - even better retrieve the invoice from the amount and check that the amount is the same as the invoice amount
                //      

                // Input in validated based on data annotations: translate the validation errors in a more user-friendly message
                if (!ModelState.IsValid)
                    return ApiBadRequest(ModelState.ToString());

                // Improvements
                //      - check that the invoice exists => does not exist return 404
                //      - not already paid
                //      - check that the user has the right to pay the invoice and that the invoice belongs to the user

                // Call the currency exchange service to convert the amount to the target currency
                var convertedAmount = await _exchangeRateService.GetExchangeRate(paymentRequest.SourceCurrency, paymentRequest.TargetCurrency);

                // Call the invoice service mark the invoice as paid

                // Return the payment confirmation to the user
                var paymentResponse = new PaymentResponse
                {
                    ConfirmationId = 1234,
                    SourceAmount = paymentRequest.Amount,
                    SourceCurrency = paymentRequest.SourceCurrency,
                    TargetAmount = convertedAmount.Rate * paymentRequest.Amount,
                    TargetCurrency = paymentRequest.TargetCurrency
                };

                return Ok(paymentResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error while paying an invoice");
                return ApiInternalError(ex.Message);
            }
        }
    }
}