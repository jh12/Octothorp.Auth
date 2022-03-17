using Microsoft.Extensions.DependencyInjection;
using Octothorp.AspNetCore.Auth.Repositories;
using Octothorp.AspNetCore.Auth.Repositories.Debug;

namespace Octothorp.AspNetCore.Auth.Helpers;

public static class AuthHelper
{
    public static IServiceCollection AddAuthDebug(this IServiceCollection builder)
    {
        builder.AddHttpContextAccessor();

        builder.AddSingleton<IUserRepository, DebugUserRepository>();

        return builder;
    }
}