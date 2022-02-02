using HeroesWebApiDemo.Dtos.V1.Responses;
using MediatR;

namespace HeroesWebApiDemo.Queries;

public class GetAllHeroesQuery : IRequest<IEnumerable<HeroResponseDto>>
{
    
}