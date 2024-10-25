using Simple.Commerce.Domain.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Simple.Commerce.Application.Models.Dtos.Products
{
    public class UpsertProductDto : IProductDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Prodcut Name must be not empty")]
        public required string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Prodcut Description must be not empty")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Prodcut Price must be zero or more"), Range(0, int.MaxValue)]
        public required decimal Price { get; set; }

        [Required(ErrorMessage = "Prodcut Stock must be zero or more"), Range(0, int.MaxValue)]
        public required int Stock { get; set; }

        public required Guid OrderId { get; set; }
    }
}
