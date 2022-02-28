using Autofac;
using Autofac.Extensions.DependencyInjection;
using Octothorp.AspNetCore.Helpers;
using Octothorp.Auth.Config;
using Octothorp.Auth.Workers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) =>
{
    lc.ReadFrom.Configuration(ctx.Configuration);
});
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(AutofacConfiguration.Configure);

// Services
IServiceCollection services = builder.Services;

services.AddOptions();
services.AddControllers();
services.AddCommonSwagger();

services.AddOpenIdAuthentication(builder.Environment, builder.Configuration);

services.AddHostedService<StartupWorker>();

// WebApplication
var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseCommonSwagger(uiOptions: uiOptions =>
{
    uiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    uiOptions.RoutePrefix = string.Empty;
});

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/test", () => "Hello World!");

app.MapControllers();
app.MapStatus();

app.Run();
