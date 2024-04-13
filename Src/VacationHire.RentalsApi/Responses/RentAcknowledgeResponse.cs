using Newtonsoft.Json;

namespace VacationHire.RentalsApi.Responses
{
    /// <summary>
    ///     A rental request needs to be approved or rejected by an administrator
    /// </summary>
    public class RentAcknowledgeResponse
    {
        public RentAcknowledgeResponse(int rentRequestId, int rentableAssetId)
        {
            RentRequestId = rentRequestId;
            RentableAssetId = rentableAssetId;
            RequestDate = DateTime.UtcNow;
        }

        /// <summary>
        ///     Each request has a unique identifier
        /// </summary>
        public int RentRequestId { get; set; }

        /// <summary>
        ///    The status of the request: Pending, Approved, Rejected, Error
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        ///     Date when the request was issued (utc)
        /// </summary>
        public DateTime RequestDate { get; set; }

        /// <summary>
        ///     The id of the object being rented
        /// </summary>
        public int RentableAssetId { get; set; }

        /// <summary>
        ///     Message displayed to the user
        /// </summary>
        public string Message { get; set; }

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