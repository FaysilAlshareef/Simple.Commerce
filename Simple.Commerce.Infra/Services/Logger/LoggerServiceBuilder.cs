using Serilog;
using Serilog.Debugging;
using Simple.Commerce.Infra.Setup;

namespace Simple.Commerce.Infra.Services.Logger
{
    public class LoggerServiceBuilder
    {
        public static ILogger Build()
        {
            var configuration = AppConfiguration.Build();
            var loggerConfiguration = configuration.GetSection("Logger");

            var appName = loggerConfiguration["AppName"];

            var logger = new LoggerConfiguration()
                .Enrich.WithProperty("name", appName)
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(configuration);

            logger.WriteTo.Console();

            SelfLog.Enable(Console.Error);
            return logger.CreateLogger();
        }
    }
}
