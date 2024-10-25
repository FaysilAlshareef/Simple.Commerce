using Serilog;
using Simple.Commerce.Api.Extensions;
using Simple.Commerce.Api.Middlewares;
using Simple.Commerce.Api.Swagger;
using Simple.Commerce.Infra;
using Simple.Commerce.Infra.Services.Logger;

namespace Simple.Commerce.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = LoggerServiceBuilder.Build();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerServices();

            builder.Services.ValidateModels();

            builder.Services.RegisterInfraService(builder.Configuration);

            builder.Host.UseSerilog();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseMiddleware<ThreadCultureMiddleware>();

            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerDocumentation();
            }

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
