using System.Net;

namespace Simple.Commerce.Domain.Exceptions
{
    public class AppException(HttpStatusCode statusCode, string message) : Exception(message)
    {
        public HttpStatusCode StatusCode { get; } = statusCode;
    }
}
