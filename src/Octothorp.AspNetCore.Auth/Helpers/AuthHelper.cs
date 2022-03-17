using Microsoft.Extensions.DependencyInjection;
using Octothorp.AspNetCore.Auth.Repositories;
using Octothorp.AspNetCore.Auth.Repositories.Test;

namespace Octothorp.AspNetCore.Auth.Helpers;

public static class AuthHelper
{
    public static IServiceCollection AddAuthTest(this IServiceCollection builder)
    {
        builder.AddHttpContextAccessor();

        builder.AddSingleton<IUserRepository, TestUserRepository>();

        return builder;
    }
}