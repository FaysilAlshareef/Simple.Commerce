using Simple.Commerce.Domain.Resources;
using System.Net;

namespace Simple.Commerce.Application.Models
{
    public class ApiResponse<T>
    {
        public bool IsSuccessed { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }

        public ApiResponse(HttpStatusCode statusCode = HttpStatusCode.OK, string? message = null)
        {
            IsSuccessed = Successed(statusCode);
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessgeForStatusCode(statusCode);
        }
        public ApiResponse(T? data, string? message = null)
        {
            StatusCode = HttpStatusCode.OK;
            IsSuccessed = Successed(StatusCode);
            Data = data;
            Message = message ?? GetDefaultMessgeForStatusCode(StatusCode);
        }
        private static string? GetDefaultMessgeForStatusCode(HttpStatusCode statusCode)
        {
            return statusCode switch
            {
                HttpStatusCode.OK => Phrases.Done,
                HttpStatusCode.BadRequest => Phrases.BadRequest,
                HttpStatusCode.Unauthorized => Phrases.Unauthrized,
                HttpStatusCode.NotFound => Phrases.NotFound,
                _ => Phrases.ServerError
            };
        }
        private static bool Successed(HttpStatusCode statusCode)
            => statusCode == HttpStatusCode.OK;
    }
}
