using AutoMapper;
using HeroesWebApiDemo.Dtos.V1.Responses;
using HeroesWebApiDemo.Queries;
using HeroesWebApiDemo.Services;
using MediatR;

namespace HeroesWebApiDemo.Handlers;

public class GetAllHeroesQueryHandler : IRequestHandler<GetAllHeroesQuery, IEnumerable<HeroResponseDto>>
{
    private readonly IHeroService _heroService;
    private readonly IMapper _mapper;

    public GetAllHeroesQueryHandler(IHeroService heroService, IMapper mapper)
    {
        _heroService = heroService;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<HeroResponseDto>> Handle(GetAllHeroesQuery request, CancellationToken cancellationToken)
    {
        return (await _heroService.GetAllHeroesAsync())
            .Select(h => _mapper.Map<HeroResponseDto>(h));
    }
}