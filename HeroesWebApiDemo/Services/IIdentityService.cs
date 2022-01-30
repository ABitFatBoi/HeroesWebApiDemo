using HeroesWebApiDemo.Entities;

namespace HeroesWebApiDemo.Services;

public interface IIdentityService
{
    Task<AuthenticationResult> RegisterAsync(string userName, string email, string password);
    Task<AuthenticationResult> LoginAsync(string? userName, string? email, string password);
}