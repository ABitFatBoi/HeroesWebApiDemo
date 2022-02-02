using AutoMapper;
using HeroesWebApiDemo.Commands;
using HeroesWebApiDemo.Dtos.V1.Responses;
using HeroesWebApiDemo.Entities;
using JetBrains.Annotations;

namespace HeroesWebApiDemo.Profiles;

[UsedImplicitly]
public class HeroesProfile : Profile
{
    public HeroesProfile()
    {
        CreateMap<HeroCreateCommand, Hero>();
        CreateMap<HeroUpdateCommand, Hero>();
        CreateMap<Hero, HeroResponseDto>();
    }
}