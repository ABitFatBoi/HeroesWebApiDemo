using HeroesWebApiDemo.Cache;
using HeroesWebApiDemo.Commands;
using HeroesWebApiDemo.Dtos.V1.Requests;
using HeroesWebApiDemo.Queries;
using HeroesWebApiDemo.Routes.V1;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace HeroesWebApiDemo.Controllers.V1;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Produces("application/json")]
public class HeroesController : ControllerBase
{
    private readonly IMediator _mediator;

    public HeroesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet(ApiRoutes.Heroes.GetAll)]
    [Cache(10)]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllHeroesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet(ApiRoutes.Heroes.GetById, Name = "GetHeroById")]
    [Cache(10)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetHeroByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result is null) return NotFound("Can't find hero with that id.");
        
        return Ok(result);
    }

    [HttpPost(ApiRoutes.Heroes.Create)]
    public async Task<IActionResult> Create([FromBody] HeroCreateDto heroCreateDto)
    {
        var command = new HeroCreateCommand(heroCreateDto);
        var result = await _mediator.Send(command);
        
        if (result is null) return BadRequest("Could not create new hero.");
        
        return CreatedAtAction(nameof(GetById), new { id = result.Id },result);
    }

    [HttpPut(ApiRoutes.Heroes.Update)]
    public async Task<IActionResult> Update(Guid id, [FromBody] HeroUpdateDto heroUpdateDto)
    {
        var command = new HeroUpdateCommand(id, heroUpdateDto);
        var result = await _mediator.Send(command);
        
        if (result is null) return NotFound("Hero hasn't been updated");
        
        return Ok(result);
    }

    [HttpDelete(ApiRoutes.Heroes.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new HeroDeleteCommand(id);
        var result = await _mediator.Send(command);
        return result ? NoContent() : NotFound("Could not find hero with that id.");
    }
}