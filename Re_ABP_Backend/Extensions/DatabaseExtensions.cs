using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Re_ABP_Backend.Data.DB;

public static class DatabaseExtensions
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        if (environment.IsProduction())
        {
            var keyVaultURL = configuration.GetSection("KeyVault:KeyVaultURL");
            var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(new AzureServiceTokenProvider().KeyVaultTokenCallback));

            // Use the AzureKeyVaultConfigurationExtensions.AddAzureKeyVault here
            configuration = new ConfigurationBuilder()
                .AddConfiguration(configuration)
                .AddAzureKeyVault(keyVaultURL.Value!.ToString(), new DefaultKeyVaultSecretManager())
                .Build();

            var client = new SecretClient(new Uri(keyVaultURL.Value!.ToString()), new DefaultAzureCredential());

            services.AddDbContext<AppDBContext>(options =>
            {
                options.UseSqlServer(client.GetSecret("ProdConnection").Value.Value.ToString());
            });
        }
        else if (environment.IsDevelopment())
        {
            services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
