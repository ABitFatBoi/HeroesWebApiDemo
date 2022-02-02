using AutoMapper;
using HeroesWebApiDemo.Commands;
using HeroesWebApiDemo.Dtos.V1.Responses;
using HeroesWebApiDemo.Entities;
using HeroesWebApiDemo.Services;
using MediatR;

namespace HeroesWebApiDemo.Handlers;

public class HeroCreateCommandHandler : IRequestHandler<HeroCreateCommand, HeroResponseDto?>
{
    private readonly IHeroService _heroService;
    private readonly IMapper _mapper;

    public HeroCreateCommandHandler(IHeroService heroService, IMapper mapper)
    {
        _heroService = heroService;
        _mapper = mapper;
    }
    
    public async Task<HeroResponseDto?> Handle(HeroCreateCommand request, CancellationToken cancellationToken)
    {
        var hero = _mapper.Map<Hero>(request);
        var created = await _heroService.CreateHeroAsync(hero);

        return !created ? null : _mapper.Map<HeroResponseDto>(hero);
    }
}