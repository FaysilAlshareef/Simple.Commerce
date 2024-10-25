using Microsoft.AspNetCore.Mvc;
using Simple.Commerce.Application.Models.Exceptions;

namespace Simple.Commerce.Api.Extensions
{
    public static class ModelStateExtensions
    {
        public static void ValidateModels(this IServiceCollection services)
            => services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                                              .Where(M => M.Value != null && M.Value.Errors.Count > 0)
                                              .SelectMany(M => M.Value!.Errors)
                                              .Select(E => E.ErrorMessage)
                                              .ToArray();

                    var ErrorResponse = new ApiValidationErorrResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(ErrorResponse);
                };
            });
    }
}
