using Newtonsoft.Json;
using VacationHire.RentalService.Responses;

namespace VacationHire.RentalsApi.Responses
{
    /// <summary>
    ///     When an item gets returned, a verification workflow is triggered
    /// - the item is checked for damages
    /// - the general status of the item is checked
    /// - an invoice is issued based on the state of the asset
    /// - customer gets a notification to pay the invoice
    /// </summary>
    public class ReturnAcknowledgeResponse
    {
        public ReturnAcknowledgeResponse(ReturnRequestResult result)
        {
            ReturnRequestId = result.ReturnRequestId;
            ReturnedDate = DateTime.UtcNow;
        }

        public int ReturnRequestId { get; set; }

        public DateTime ReturnedDate { get; set; }

        /// <summary>
        ///     Override TOString method to serialize the object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}