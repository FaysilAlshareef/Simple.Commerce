using Simple.Commerce.Application.Models.Exceptions;
using Simple.Commerce.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace Simple.Commerce.Api.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;
        private readonly IHostEnvironment _env = env;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (AppException appException)
            {
                var errorResponse = new ApiExceptionResponse(appException.StatusCode, appException.Message);

                await ErrorWriterAsync(context, errorResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: ex.Message);


                // To Create error Response that be Different from Development to other
                var errorResponse = _env.IsDevelopment() ?
                    new ApiExceptionResponse(HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString())
                    :
                    new ApiExceptionResponse(HttpStatusCode.InternalServerError);

                await ErrorWriterAsync(context, errorResponse);
            }
        }

        public async Task ErrorWriterAsync(HttpContext context, ApiExceptionResponse errorResponse)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)errorResponse.StatusCode;

            // to send json with camel case proprty
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            // To Convert errorresponse to json File
            var json = JsonSerializer.Serialize(errorResponse, options);

            //// send json File
            await context.Response.WriteAsync(json);
        }
    }
}
