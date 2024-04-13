namespace VacationHire.Common.GenericResponses
{
    /// <summary>
    ///     Return responses for HttpStatusCode = 400
    /// </summary>
    public class ApiBadRequestResult : ApiNotOkResult
    {
        private const int BadRequestStatusCode = 400;

        /// <summary>
        ///     Set the HttpStatusCode to 400
        /// </summary>
        /// <param name="message">Explanatory message</param>
        /// <param name="internalErrorCode">Internal error message</param>
        public ApiBadRequestResult(string message, string internalErrorCode = "") : base(message, internalErrorCode)
        {
            HttpStatusCode = BadRequestStatusCode;
        }
    }
}