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
            return BadRequest();
        }

        throw new NotImplementedException();
    }
}