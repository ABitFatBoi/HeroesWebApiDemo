using AutoMapper;
using HeroesWebApiDemo.Dtos;
using HeroesWebApiDemo.Entities;

namespace HeroesWebApiDemo.Profiles;

public class HeroesProfile : Profile
{
    public HeroesProfile()
    {
        CreateMap<HeroCreateDto, Hero>();
        CreateMap<HeroUpdateDto, Hero>();
        CreateMap<Hero, HeroResponseDto>();
    }
}