using System.Net;

namespace Simple.Commerce.Application.Models.Exceptions
{
    public class ApiExceptionResponse(
        HttpStatusCode statusCode,
        string? massege = null,
        string? details = null) : ApiResponse<object>(statusCode, massege)
    {
        public string? Details { get; } = details;
    }
}
