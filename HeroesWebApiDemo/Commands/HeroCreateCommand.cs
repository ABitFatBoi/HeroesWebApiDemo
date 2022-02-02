using HeroesWebApiDemo.Dtos.V1.Requests;
using HeroesWebApiDemo.Dtos.V1.Responses;
using HeroesWebApiDemo.Enums;
using MediatR;

namespace HeroesWebApiDemo.Commands;

public class HeroCreateCommand : IRequest<HeroResponseDto>
{
    public string Name { get; }
    public bool IsMelee { get; }
    public HeroType Type { get; }

    public HeroCreateCommand(HeroCreateDto heroCreateDto)
    {
        Name = heroCreateDto.Name;
        IsMelee = heroCreateDto.IsMelee;
        Type = heroCreateDto.Type;
    }
}