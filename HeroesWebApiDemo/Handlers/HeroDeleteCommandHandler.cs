using HeroesWebApiDemo.Commands;
using HeroesWebApiDemo.Services;
using JetBrains.Annotations;
using MediatR;

namespace HeroesWebApiDemo.Handlers;

[UsedImplicitly]
public class HeroDeleteCommandHandler : IRequestHandler<HeroDeleteCommand, bool>
{
    private readonly IHeroService _heroService;

    public HeroDeleteCommandHandler(IHeroService heroService)
    {
        _heroService = heroService;
    }
    
    public async Task<bool> Handle(HeroDeleteCommand request, CancellationToken cancellationToken)
    {
        return await _heroService.DeleteHeroAsync(request.Id);
    }
}