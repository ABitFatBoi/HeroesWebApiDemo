using AutoMapper;
using HeroesWebApiDemo.Commands;
using HeroesWebApiDemo.Dtos.V1.Requests;
using HeroesWebApiDemo.Dtos.V1.Responses;
using HeroesWebApiDemo.Entities;

namespace HeroesWebApiDemo.Profiles;

public class HeroesProfile : Profile
{
    public HeroesProfile()
    {
        CreateMap<HeroCreateCommand, Hero>();
        CreateMap<HeroUpdateDto, Hero>();
        CreateMap<Hero, HeroResponseDto>();
    }
}