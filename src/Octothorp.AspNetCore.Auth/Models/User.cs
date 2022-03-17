namespace Octothorp.AspNetCore.Auth.Models;

public class User
{
    public Guid Id { get; }

    public string Username { get; }
    public string Nickname { get; }

    public User(Guid id, string username, string nickname)
    {
        Id = id;
        Username = username;
        Nickname = nickname;
    }
}