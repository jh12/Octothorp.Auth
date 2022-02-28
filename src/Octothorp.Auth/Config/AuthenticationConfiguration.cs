using Microsoft.EntityFrameworkCore;
using Octothorp.Auth.EF;
using OpenIddict.Validation.AspNetCore;

namespace Octothorp.Auth.Config;

internal static class AuthenticationConfiguration
{
    public static IServiceCollection AddOpenIdAuthentication(this IServiceCollection services, IHostEnvironment environment, IConfiguration configuration)
    {
        services.AddOpenIddict()
            .AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                    .UseDbContext<ApplicationDbContext>()
                    .ReplaceDefaultEntities<Guid>();
            })
            .AddServer(options =>
            {
                options.SetTokenEndpointUris("/connect/token");

                options.AllowClientCredentialsFlow();

                options.AddDevelopmentEncryptionCertificate()
                    .AddDevelopmentSigningCertificate();

                options.UseAspNetCore()
                    .DisableTransportSecurityRequirement()
                    .EnableTokenEndpointPassthrough();
            })
            .AddValidation(options =>
            {
                options.UseLocalServer();
                options.UseAspNetCore();
            });

        services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            string sqliteConnectionString = configuration["Sqlite:OpenId:ConnectionString"] ?? "Data Source = " + Path.Combine(environment.ContentRootPath, "Data", "OpenIdDict.db");

            options.UseSqlite(sqliteConnectionString);
            options.UseOpenIddict<Guid>();
        });

        return services;
    }
}