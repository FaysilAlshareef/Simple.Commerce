namespace Simple.Commerce.Application.Models.FilterModels
{
    public record GetOrdersFilterModel(
        Guid? CustomerId,
        DateTime? OrderDate,
        int PageSize = 25,
        int CurrentPage = 1);
}
