namespace Simple.Commerce.Application.Models.Exceptions
{
    public class ApiValidationErorrResponse() : ApiResponse<object>(System.Net.HttpStatusCode.BadRequest)
    {
        public IEnumerable<string> Errors { get; set; } = [];
    }
}
