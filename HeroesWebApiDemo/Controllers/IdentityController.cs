using HeroesWebApiDemo.Dtos;
using HeroesWebApiDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeroesWebApiDemo.Controllers;

[ApiController]
[Produces("application/json")]
[Route("[controller]")]
public class IdentityController : Controller
{
    private readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("/register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDto userRegistrationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new AuthenticationFailedDto
            {
                ErrorMessages = ModelState.Values.SelectMany(
                    v => v.Errors.Select(e => e.ErrorMessage))
            });
        }

        var authenticationResult = await _identityService.RegisterAsync(
            userRegistrationDto.UserName, userRegistrationDto.Email, userRegistrationDto.Password);

        if (authenticationResult.ErrorMessages is not null)
        {
            return BadRequest(new AuthenticationFailedDto
            {
                ErrorMessages = authenticationResult.ErrorMessages
            });
        }
        
        return Ok(new AuthenticationSuccessDto
        {
            Token = authenticationResult.Token!
        });
    }
    
    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var authenticationResult = await _identityService.LoginAsync(
            userLoginDto.UserName, userLoginDto.Email, userLoginDto.Password);

        if (authenticationResult.ErrorMessages is not null)
        {
            return BadRequest(new AuthenticationFailedDto
            {
                ErrorMessages = authenticationResult.ErrorMessages
            });
        }
        
        return Ok(new AuthenticationSuccessDto
        {
            Token = authenticationResult.Token!
        });
    }
}