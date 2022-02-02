using HeroesWebApiDemo.Dtos.V1.Requests;
using HeroesWebApiDemo.Dtos.V1.Responses;
using HeroesWebApiDemo.Enums;
using MediatR;

namespace HeroesWebApiDemo.Commands;

public class HeroUpdateCommand : IRequest<HeroResponseDto>
{
    public Guid Id { get; }
    public string Name { get; }
    public bool IsMelee { get; }
    public HeroType Type { get; }

    public HeroUpdateCommand(Guid id, HeroUpdateDto heroCreateDto)
    {
        Id = id;
        Name = heroCreateDto.Name;
        IsMelee = heroCreateDto.IsMelee;
        Type = heroCreateDto.Type;
    }
}