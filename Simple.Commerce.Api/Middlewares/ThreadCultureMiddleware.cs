using Microsoft.Extensions.Primitives;
using System.Globalization;

namespace Simple.Commerce.Api.Middlewares
{
    public class ThreadCultureMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            var headerLanguage = context.Request.Headers.FirstOrDefault(t => t.Key == "language");

            if (headerLanguage != StringValues.Empty)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(headerLanguage.Value.ToString());
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(headerLanguage.Value.ToString());
            }

            await _next.Invoke(context);
        }
    }
}
