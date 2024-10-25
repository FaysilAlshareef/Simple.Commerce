using Microsoft.AspNetCore.Mvc;

namespace Simple.Commerce.Api.Controllers.V1
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public abstract class ControllerBaseV1 : ControllerBase
    {
        public const string DefaultRoute = "api/v1/[controller]";
        public const string RoutePrefix = "api/v1/";
    }
}
