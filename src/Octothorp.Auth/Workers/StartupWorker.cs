using Octothorp.Auth.EF;
using OpenIddict.Abstractions;

namespace Octothorp.Auth.Workers;

public class StartupWorker : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public StartupWorker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        await SeedOpenIdContext(scope);
    }

    private async Task SeedOpenIdContext(IServiceScope serviceScope)
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync();

        var manager = serviceScope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        if (await manager.FindByClientIdAsync("admin") == null)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "admin",
                ClientSecret = "388D45FA-B36B-4988-BA59-B187D329C207",
                DisplayName = "Admin account",
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.GrantTypes.ClientCredentials
                }
            });
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}