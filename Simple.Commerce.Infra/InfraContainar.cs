using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Simple.Commerce.Application.Contracts.Repositories;
using Simple.Commerce.Application.Contracts.Services;
using Simple.Commerce.Infra.Persistence.DbRegistration;
using Simple.Commerce.Infra.Persistence.Repositories;
using Simple.Commerce.Infra.Services.BaseServices;

namespace Simple.Commerce.Infra
{
    public static class InfraContainar
    {
        public static void RegisterInfraService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlDb(configuration);

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
