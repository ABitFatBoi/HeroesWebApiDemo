using AutoMapper;
using HeroesWebApiDemo.Dtos.Requests;
using HeroesWebApiDemo.Dtos.Responses;
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