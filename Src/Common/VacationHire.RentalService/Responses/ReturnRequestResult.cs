namespace VacationHire.RentalService.Responses
{
    /// <summary>
    ///     This is an internal response to a return request
    /// </summary>
    public class ReturnRequestResult
    {
        /// <summary>
        ///     Dummy generated
        /// </summary>
        public int ReturnRequestId { get; set; }

        /// <summary>
        ///     The id of the object being rented
        /// </summary>
        public int RentableAssetId { get; set; }

        // For tracking purposes: more information should be added here: RentRequestId
    }
}