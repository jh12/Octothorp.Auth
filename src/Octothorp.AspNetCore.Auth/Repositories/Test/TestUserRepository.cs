using Microsoft.AspNetCore.Http;
using Octothorp.AspNetCore.Auth.Models;

namespace Octothorp.AspNetCore.Auth.Repositories.Test;

public class TestUserRepository : IUserRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private static User DebugUser = new User(Guid.Parse("11111111-1111-1111-1111-111111111111"), "Debugger", "DebugMcDebug");

    public TestUserRepository(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<User> GetCurrentUser()
    {
        HttpContext? context = _httpContextAccessor.HttpContext;

        if (context == null || (context.User.Identity != null && !context.User.Identity!.IsAuthenticated))
        {
            return Task.FromResult(DebugUser);
        }
        else
        {
            throw new NotImplementedException();
        }
    }
}