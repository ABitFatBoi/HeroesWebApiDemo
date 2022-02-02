using AutoMapper;
using HeroesWebApiDemo.Dtos.V1.Responses;
using HeroesWebApiDemo.Queries;
using HeroesWebApiDemo.Services;
using JetBrains.Annotations;
using MediatR;

namespace HeroesWebApiDemo.Handlers;

[UsedImplicitly]
public class GetHeroByIdQueryHandler : IRequestHandler<GetHeroByIdQuery, HeroResponseDto?>
{
    private readonly IHeroService _heroService;
    private readonly IMapper _mapper;

    public GetHeroByIdQueryHandler(IHeroService heroService, IMapper mapper)
    {
        _heroService = heroService;
        _mapper = mapper;
    }
    
    public async Task<HeroResponseDto?> Handle(GetHeroByIdQuery request, CancellationToken cancellationToken)
    {
        var hero =  await _heroService.GetHeroByIdAsync(request.Id);
        return _mapper.Map<HeroResponseDto>(hero);
    }
}