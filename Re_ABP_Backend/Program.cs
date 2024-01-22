using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Re_ABP_Backend;
using Re_ABP_Backend.Data.DB;
using Re_ABP_Backend.Data.Interfraces;
using Re_ABP_Backend.Exntensions;
using Re_ABP_Backend.Extensions;
using Re_ABP_Backend.Middleware;
using Serilog;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddLocalization();

var locazalitionOption = new RequestLocalizationOptions();

var supportedCultures = new[]
{
    new CultureInfo("en-US"),
    new CultureInfo("uk-UA")
};

locazalitionOption.SupportedCultures = supportedCultures;
locazalitionOption.SupportedUICultures = supportedCultures;
locazalitionOption.SetDefaultCulture("en-US");
locazalitionOption.ApplyCurrentCultureToResponseHeaders = true;

builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("ApplicationSettings"));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();
builder.Services.AddCorsConfiguration();
builder.Services.AddIdentityServices(builder.Configuration);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/action-logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

if (builder.Environment.IsProduction())
{
    var keyVaultURL = builder.Configuration.GetSection("KeyVault:KeyVaultURL");

    var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(new AzureServiceTokenProvider().KeyVaultTokenCallback));

    builder.Configuration.AddAzureKeyVault(keyVaultURL.Value!.ToString(), new DefaultKeyVaultSecretManager());

    var client = new SecretClient(new Uri(keyVaultURL.Value!.ToString()), new DefaultAzureCredential());

    builder.Services.AddDbContext<AppDBContext>(options =>
    {
        options.UseSqlServer(client.GetSecret("ProdConnection").Value.Value.ToString());
    });
}

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}

var app = builder.Build();

app.UseRequestLocalization(locazalitionOption);

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


// Create db if it not alredy exists
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<AppDBContext>();
var userService = services.GetRequiredService<IUserService>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await AppDbContextSeed.SeedAsync(context, userService);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
