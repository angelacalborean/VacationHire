using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VacationHire.Authentication;
using VacationHire.Authentication.Auth;
using VacationHire.Authentication.Interfaces;
using VacationHire.CurrencyExchangeService;
using VacationHire.CurrencyExchangeService.Interface;
using VacationHire.Data.Data;
using VacationHire.QueueManagement;
using VacationHire.QueueManagement.Interfaces;
using VacationHire.RentalService.Implementations;
using VacationHire.RentalService.Interfaces;
using VacationHire.Repository;
using VacationHire.Repository.Interfaces;

namespace VacationHire.RentalsApi
{

    /// <summary>
    ///     There is duplicated code between the different APIs. This code can be refactored into a shared library.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddDbContext<VacationHireDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("VacationHireDbContext")));
            }
            else
            {
                // Read connection string from Azure Key Vault and use it to connect to the database

                var keyVaultUrl = new Uri(builder.Configuration["AzureKeyVaultUrl"]);
                var secretClient = new SecretClient(keyVaultUrl, new DefaultAzureCredential());

                KeyVaultSecret connectionString = secretClient.GetSecret("YourConnectionStringSecretName");
                builder.Services.AddDbContext<VacationHireDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString(connectionString.Value)));
            }

            builder.Services.AddScoped<ICarAssetRepository, CarAssetRepository>();
            builder.Services.AddScoped<ICabinAssetRepository, CabinAssetRepository>();

            builder.Services.AddScoped<IRentableService, CarAssetsRentableService>();
            builder.Services.AddScoped<IRentableService, CabinAssetsRentableService>();

            builder.Services.AddScoped<IRentableServiceFactory, RentableServiceFactory>();

            builder.Services.AddScoped<IQueueClientManager, QueueClientManagerManager>();

            builder.Services.AddHttpClient<IExchangeRateService, ExchangeRateRateService>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ExchangeServiceBaseUrl"]);
            }); //retry policies can be added here

            // Here we can see the dummy roles added for this demo: for ease of testing
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                // Allow authentication via swagger
                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basic"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // Test it with  "user@vacationhire.com" && "DummyPassword")
            builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            // Acts like a middleware to transform the claims (called before code is executed on controllers)
            builder.Services.AddTransient<IClaimsTransformation, ClaimsTransformationService>();

            builder.Services.AddAuthorization();

            var app = builder.Build();

            //// Minimal api for login testing: hardcoded values for roles; I have added this here to be able to call in from integration tests
            //// If you want to test it, uncomment the code below and call it from the integration tests - while testing I started this from a different api
            //// Otherwise, use it from the swagger UI
            //app.MapGet("/login", (string username) =>
            //{
            //    var claimsPrincipal = new ClaimsPrincipal(
            //        new ClaimsIdentity(new[]
            //            {
            //                new Claim(ClaimTypes.Name, username),
            //                new Claim(ClaimTypes.Role, "User")
            //            },
            //            BearerTokenDefaults.AuthenticationScheme));

            //    return Results.SignIn(claimsPrincipal);
            //});

            //app.MapGet("/user", (ClaimsPrincipal user) => Results.Ok($"Welcome {user.Identity.Name}!"))
            //    .RequireAuthorization();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}