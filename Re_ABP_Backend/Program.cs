using Microsoft.EntityFrameworkCore;
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
builder.Services.AddDatabase(builder.Configuration, builder.Environment); // Add this line

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/action-logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

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
