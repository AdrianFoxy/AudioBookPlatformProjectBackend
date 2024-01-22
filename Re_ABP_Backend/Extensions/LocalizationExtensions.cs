using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace Re_ABP_Backend.Extensions
{
    public static class LocalizationExtensions
    {
        public static IServiceCollection AddCustomLocalization(this IServiceCollection services)
        {
            services.AddLocalization();

            var localizationOptions = new RequestLocalizationOptions();

            var supportedCultures = new[]
            {
            new CultureInfo("en-US"),
            new CultureInfo("uk-UA")
        };

            localizationOptions.SupportedCultures = supportedCultures;
            localizationOptions.SupportedUICultures = supportedCultures;
            localizationOptions.SetDefaultCulture("en-US");
            localizationOptions.ApplyCurrentCultureToResponseHeaders = true;

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.ApplyCurrentCultureToResponseHeaders = true;
            });

            return services;
        }

        public static IApplicationBuilder UseCustomLocalization(this IApplicationBuilder app)
        {
            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();

            if (localizationOptions != null)
                app.UseRequestLocalization(localizationOptions.Value);

            return app;
        }
    }

}
