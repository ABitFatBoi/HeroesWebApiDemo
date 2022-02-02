using HeroesWebApiDemo.Dtos.V1.Responses;
using MediatR;

namespace HeroesWebApiDemo.Queries;

public class GetHeroByIdQuery : IRequest<HeroResponseDto>
{
    public Guid Id { get; }

    public GetHeroByIdQuery(Guid id)
    {
        Id = id;
    }
}