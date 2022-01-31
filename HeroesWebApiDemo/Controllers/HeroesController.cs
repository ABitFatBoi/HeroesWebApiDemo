using AutoMapper;
using HeroesWebApiDemo.Dtos;
using HeroesWebApiDemo.Entities;
using HeroesWebApiDemo.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeroesWebApiDemo.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Produces("application/json")]
[Route("[controller]")]
public class HeroesController : ControllerBase
{
    private readonly IHeroService _heroService;
    private readonly IMapper _mapper;

    public HeroesController(IHeroService heroService, IMapper mapper)
    {
        _heroService = heroService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var heroes = (await _heroService.GetAllHeroesAsync())
            .Select(h => _mapper.Map<HeroResponseDto>(h));

        return Ok(heroes);
    }
    
    [HttpGet("{id}", Name = "GetHeroById")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var hero = await _heroService.GetHeroByIdAsync(id);
        
        if (hero == null) return NotFound("Could not find hero with that id.");
        
        return Ok(_mapper.Map<HeroResponseDto>(hero));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] HeroCreateDto heroCreateDto)
    {
        var hero = _mapper.Map<Hero>(heroCreateDto);
        var created = await _heroService.CreateHeroAsync(hero);

        if (!created) return BadRequest("Could not create new hero.");

        var responseDto = _mapper.Map<HeroResponseDto>(hero);

        return CreatedAtAction(nameof(GetById), new { id = responseDto.Id },responseDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] HeroUpdateDto heroUpdateDto)
    {
        var hero = _mapper.Map<Hero>(heroUpdateDto);
        hero.Id = id;
        
        var updated = await _heroService.UpdateHeroAsync(hero);

        return updated ? Ok(_mapper.Map<HeroResponseDto>(hero)) : NotFound("Hero hasn't been updated");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _heroService.DeleteHeroAsync(id);
        
        return deleted ? NoContent() : NotFound("Could not find hero with that id.");
    }
}