﻿using AutoMapper;
using HeroesWebApiDemo.Commands;
using HeroesWebApiDemo.Dtos.V1.Responses;
using HeroesWebApiDemo.Entities;
using HeroesWebApiDemo.Services;
using JetBrains.Annotations;
using MediatR;

namespace HeroesWebApiDemo.Handlers;

[UsedImplicitly]
public class HeroUpdateCommandHandler : IRequestHandler<HeroUpdateCommand, HeroResponseDto?>
{
    private readonly IHeroService _heroService;
    private readonly IMapper _mapper;

    public HeroUpdateCommandHandler(IHeroService heroService, IMapper mapper)
    {
        _heroService = heroService;
        _mapper = mapper;
    }
    
    public async Task<HeroResponseDto?> Handle(HeroUpdateCommand request, CancellationToken cancellationToken)
    {
        var hero = _mapper.Map<Hero>(request);
        var updated = await _heroService.UpdateHeroAsync(hero);

        return !updated ? null : _mapper.Map<HeroResponseDto>(hero);
    }
}