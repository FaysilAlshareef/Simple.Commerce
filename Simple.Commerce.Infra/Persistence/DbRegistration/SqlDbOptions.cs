using System.ComponentModel.DataAnnotations;

namespace Simple.Commerce.Infra.Persistence.DbRegistration
{
    public class SqlDbOptions
    {
        public const string SqlDb = "ConnectionStrings";

        [Required]
        public required string Database { get; init; }
    }
}
