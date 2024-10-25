namespace Simple.Commerce.Application.Models
{
    public class Paginated<T> where T : class
    {
        public required int CurrentPage { get; set; }
        public required int PageSize { get; set; }
        public required int Total { get; set; }
        public required List<T> Results { get; set; } = [];
    }
}
