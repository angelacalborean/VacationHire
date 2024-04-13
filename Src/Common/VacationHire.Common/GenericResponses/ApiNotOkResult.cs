
using Microsoft.AspNetCore.Mvc;

namespace VacationHire.Common.GenericResponses
{
    /// <summary>
    ///     Use this result for all situations when something different from 200 (ok) needs to be returned.
    /// This might be needed to ensure that all your responses are consistent and follow the same pattern.
    /// Different ErrorCode / ErrorObject can be used to the user, but the format of the response should be the same.
    /// </summary>
    public abstract class ApiNotOkResult : ObjectResult
    {
        /// <summary>
        ///     Assume you want to track the error code in across different services
        /// </summary>
        public string InternalErrorCode { get; set; }

        /// <summary>
        ///     User-friendly readable explanation of the failure
        /// </summary>
        public string Message { get; set; }

        public int? HttpStatusCode
        {
            get => StatusCode;
            set => StatusCode = value;
        }


        /// <summary>
        ///     Result returned in situations different from 200 (ok)
        /// </summary>
        /// <param name="message">User-friendly description of the problem</param>
        /// <param name="internalErrorCode">Internal error code that contains internal identifier for the problem</param>
        protected ApiNotOkResult(string message, string internalErrorCode) : base(message)
        {
            ContentTypes = ["application/json"];
            Message = message;
            InternalErrorCode = internalErrorCode;
        }

        /// <summary>
        ///     Result returned in situations different from 200 (ok)
        /// </summary>
        /// <param name="exception">Exception for the given operation</param>
        /// <param name="internalErrorCode">Internal error code that contains internal identifier for the problem</param>
        protected ApiNotOkResult(Exception exception, string internalErrorCode) : this(exception.Message, internalErrorCode)
        {
        }
    }
}