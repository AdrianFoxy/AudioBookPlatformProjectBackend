using Serilog;

namespace Re_ABP_Backend.Extensions
{
    public static class LoggerExtensions
    {
        public static void AddCustomLogger(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File("logs/action-logs.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddSingleton(Log.Logger);
        }

    }
}
