using HeroesWebApiDemo.Entities;
using Microsoft.AspNetCore.Identity;

namespace HeroesWebApiDemo.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<IdentityUser> _userManager;

    public IdentityService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<AuthenticationResult> RegisterAsync(string userName, string email, string password)
    {
        if (await CheckIfUserExistsAsync(userName, email) != null)
        {
            return new AuthenticationResult
            {
                ErrorMessages = new [] { "User with specified User name/Email already exists." }
            };
        }

        var newUser = new IdentityUser
        {
            UserName = userName,
            Email = email
        };
        
        var createdUser = await _userManager.CreateAsync(newUser, password);

        if (!createdUser.Succeeded)
        {
            return new AuthenticationResult
            {
                ErrorMessages = createdUser.Errors.Select(x => x.Description)
            };
        }

        return await GenerateAuthenticationResultAsync(newUser);
    }

    public async Task<AuthenticationResult> LoginAsync(string userName, string email, string password)
    {
        var requiredUser = await CheckIfUserExistsAsync(userName, email);
        
        if (requiredUser == null)
        {
            return new AuthenticationResult
            {
                ErrorMessages = new [] { "User with specified User name/Email does not exist." }
            };
        }

        return await GenerateAuthenticationResultAsync(requiredUser);
    }
    
    private async Task<IdentityUser?> CheckIfUserExistsAsync(string userName, string email)
    {
        var existingUser = await _userManager.FindByNameAsync(userName);

        if (existingUser == null)
        {
            existingUser = await _userManager.FindByEmailAsync(email);
        }

        return existingUser;
    }
    
    private async Task<AuthenticationResult> GenerateAuthenticationResultAsync(IdentityUser user)
    {
        throw new NotImplementedException();
    }
}