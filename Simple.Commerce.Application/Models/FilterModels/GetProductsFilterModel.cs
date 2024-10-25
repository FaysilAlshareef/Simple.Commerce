namespace Simple.Commerce.Application.Models.FilterModels
{
    public record GetProductsFilterModel(
        Guid? OrderId,
        string? Name,
        int PageSize = 25,
        int CurrentPage = 1);
}
