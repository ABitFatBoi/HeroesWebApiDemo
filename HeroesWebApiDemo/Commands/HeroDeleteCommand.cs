using MediatR;

namespace HeroesWebApiDemo.Commands;

public class HeroDeleteCommand : IRequest<bool>
{
    public Guid Id { get; }

    public HeroDeleteCommand(Guid id)
    {
        Id = id;
    }
}