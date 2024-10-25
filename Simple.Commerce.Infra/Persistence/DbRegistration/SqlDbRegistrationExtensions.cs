using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Simple.Commerce.Infra.Persistence.DbRegistration
{
    public static class SqlDbRegistrationExtensions
    {
        public static void AddSqlDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlDbOptions(configuration);

            services.AddDbContext<AppDbContext>((provider, configure) =>
            {
                var options = provider.GetRequiredService<IOptions<SqlDbOptions>>();
                configure.UseSqlServer(options.Value.Database);
            });

            services.AddHostedService<DatabaseRunner>();
        }

        private static void AddSqlDbOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<SqlDbOptions>()
                .Bind(configuration.GetSection(SqlDbOptions.SqlDb))
                .ValidateDataAnnotations()
                .ValidateOnStart();
        }

    }
}
