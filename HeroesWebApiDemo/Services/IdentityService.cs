using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HeroesWebApiDemo.Entities;
using HeroesWebApiDemo.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace HeroesWebApiDemo.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtSettings _jwtSettings;

    public IdentityService(UserManager<IdentityUser> userManager, JwtSettings jwtSettings)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings;
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

        return GenerateAuthenticationResult(newUser);
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

        return GenerateAuthenticationResult(requiredUser);
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
    
    private AuthenticationResult GenerateAuthenticationResult(IdentityUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id)
            }),
            Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return new AuthenticationResult
        {
            Token = tokenHandler.WriteToken(token)
        };
    }
}