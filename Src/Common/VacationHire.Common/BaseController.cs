using Microsoft.AspNetCore.Mvc;
using VacationHire.Common.GenericResponses;

namespace VacationHire.Common
{
    /// <summary>
    ///     There are some common methods that we want to use in all controllers
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        ///     Return responses for (int)HttpStatusCode.BadRequest = 400
        /// </summary>
        /// <param name="message">Explanatory message</param>
        /// <param name="internalErrorMessage">Internal error message</param>
        /// <returns>The created <see cref="ApiBadRequestResult" /> for the response.</returns>
        protected ApiBadRequestResult ApiBadRequest(string message, string internalErrorMessage = "")
        {
            return new ApiBadRequestResult(message, internalErrorMessage);
        }

        /// <summary>
        ///     Set the HttpStatusCode to (int)HttpStatusCode.NotFound = 404
        /// </summary>
        /// <param name="message">Explanatory message</param>
        /// <param name="internalErrorMessage">Internal error message</param>
        /// <returns>The created <see cref="ApiNotFoundResult" /> for the response.</returns>
        protected ApiNotFoundResult ApiNotFound(string message, string internalErrorMessage = "")
        {
            return new ApiNotFoundResult(message, internalErrorMessage);
        }

        /// <summary>
        ///     Set the HttpStatusCode to (int)HttpStatusCode.InternalServerError = 500
        /// </summary>
        /// <param name="message">Explanatory message</param>
        /// <param name="internalErrorCode">Internal error message</param>
        /// <returns>The created <see cref="ApiInternalErrorResult" /> for the response.</returns>
        protected ApiInternalErrorResult ApiInternalError(string message, string internalErrorCode = "")
        {
            return new ApiInternalErrorResult(message, internalErrorCode);
        }

        /// <summary>
        ///     Sometimes we want to return the exception as well
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <param name="internalErrorCode">Internal error message</param>
        /// <returns>The created <see cref="ApiInternalErrorResult" /> for the response.</returns>
        protected ApiInternalErrorResult ApiInternalError(Exception exception, string internalErrorCode = "")
        {
            return new ApiInternalErrorResult(exception, internalErrorCode);
        }
    }
}
