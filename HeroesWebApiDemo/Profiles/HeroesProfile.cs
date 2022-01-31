using AutoMapper;
using HeroesWebApiDemo.Dtos.V1.Requests;
using HeroesWebApiDemo.Dtos.V1.Responses;
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