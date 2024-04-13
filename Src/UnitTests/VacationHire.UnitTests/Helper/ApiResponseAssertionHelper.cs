using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace VacationHire.UnitTests.Helper
{
    public static class ApiResponseAssertionHelper
    {
        public static void CheckApiResponseContentType(ObjectResult result, string errorMessage)
        {
            Assert.Contains("application/json", result.ContentTypes);

            var errorDetails = result.Value as string;
            Assert.Contains(errorMessage, errorDetails);
        }
    }
}