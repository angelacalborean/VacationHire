namespace VacationHire.Common.GenericResponses
{
    /// <summary>
    ///     Return responses for HttpStatusCode = 404
    /// </summary>
    public class ApiNotFoundResult : ApiNotOkResult
    {
        private const int NotFoundStatusCode = 404;

        /// <summary>
        ///     Set the HttpStatusCode to 404
        /// </summary>
        /// <param name="message">Explanatory message</param>
        /// <param name="internalErrorCode">Internal error message</param>
        public ApiNotFoundResult(string message, string internalErrorCode = "") : base(message, internalErrorCode)
        {
            StatusCode = NotFoundStatusCode;
        }
    }
}