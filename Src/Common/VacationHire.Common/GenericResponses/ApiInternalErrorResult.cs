namespace VacationHire.Common.GenericResponses
{
    /// <summary>
    ///     Return responses for HttpStatusCode = 500
    /// </summary>
    public class ApiInternalErrorResult : ApiNotOkResult
    {
        private const int InternalErrorStatusCode = 500;

        /// <summary>
        ///     Set the HttpStatusCode to 500
        /// </summary>
        /// <param name="message">Explanatory message</param>
        /// <param name="internalErrorCode">Internal error message</param>
        public ApiInternalErrorResult(string message, string internalErrorCode) : base(message, internalErrorCode)
        {
            HttpStatusCode = InternalErrorStatusCode;
        }

        /// <summary>
        ///     Sometimes we want to return the exception as well
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <param name="internalErrorCode">Internal error message</param>
        public ApiInternalErrorResult(Exception exception, string internalErrorCode) : this(exception.Message, internalErrorCode)
        {
            HttpStatusCode = InternalErrorStatusCode;
        }
    }
}