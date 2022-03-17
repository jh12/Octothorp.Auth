using Octothorp.AspNetCore.Auth.Models;

namespace Octothorp.AspNetCore.Auth.Repositories;

public interface IUserRepository
{
    Task<User> GetCurrentUser();
}