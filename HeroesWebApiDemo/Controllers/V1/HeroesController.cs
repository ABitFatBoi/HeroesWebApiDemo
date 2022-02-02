using AutoMapper;
using HeroesWebApiDemo.Dtos.V1.Requests;
using HeroesWebApiDemo.Dtos.V1.Responses;
using HeroesWebApiDemo.Entities;
using HeroesWebApiDemo.Queries;
using HeroesWebApiDemo.Routes.V1;
using HeroesWebApiDemo.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeroesWebApiDemo.Controllers.V1;

// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Produces("application/json")]
public class HeroesController : ControllerBase
{
    private readonly IHeroService _heroService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public HeroesController(IHeroService heroService, IMapper mapper, IMediator mediator)
    {
        _heroService = heroService;
        _mapper = mapper;
        _mediator = mediator;
    }
    
    [HttpGet(ApiRoutes.Heroes.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllHeroesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet(ApiRoutes.Heroes.GetById, Name = "GetHeroById")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetHeroByIdQuery(id);
        var result = await _mediator.Send(query);
        
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        if (result is null) return NotFound("Can't find hero with that id.");
        
        return Ok(result);
    }

    [HttpPost(ApiRoutes.Heroes.Create)]
    public async Task<IActionResult> Create([FromBody] HeroCreateDto heroCreateDto)
    {
        var hero = _mapper.Map<Hero>(heroCreateDto);
        var created = await _heroService.CreateHeroAsync(hero);

        if (!created) return BadRequest("Could not create new hero.");

        var responseDto = _mapper.Map<HeroResponseDto>(hero);

        return CreatedAtAction(nameof(GetById), new { id = responseDto.Id },responseDto);
    }

    [HttpPut(ApiRoutes.Heroes.Update)]
    public async Task<IActionResult> Update(Guid id, [FromBody] HeroUpdateDto heroUpdateDto)
    {
        var hero = _mapper.Map<Hero>(heroUpdateDto);
        hero.Id = id;
        
        var updated = await _heroService.UpdateHeroAsync(hero);

        return updated ? Ok(_mapper.Map<HeroResponseDto>(hero)) : NotFound("Hero hasn't been updated");
    }

    [HttpDelete(ApiRoutes.Heroes.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _heroService.DeleteHeroAsync(id);
        
        return deleted ? NoContent() : NotFound("Could not find hero with that id.");
    }
}