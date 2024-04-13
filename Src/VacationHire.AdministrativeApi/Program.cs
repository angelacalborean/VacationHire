using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VacationHire.AdministrativeApi.Data;
using VacationHire.AdministrativeApi.Mapper;
using VacationHire.Authentication;
using VacationHire.Authentication.Auth;
using VacationHire.Authentication.Interfaces;
using VacationHire.Data.Data;
using VacationHire.Repository;
using VacationHire.Repository.Interfaces;

namespace VacationHire.AdministrativeApi
{
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

            builder.Services.AddAutoMapperService(typeof(Program).Assembly);

            builder.Services.AddScoped<IAssetRepository, AssetRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
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

            builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            // Acts like a middleware to transform the claims (called before code is executed on controllers)
            builder.Services.AddTransient<IClaimsTransformation, ClaimsTransformationService>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                DatabaseInitializer.Initialize(services);
            }

            // Configure the HTTP request pipeline.
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