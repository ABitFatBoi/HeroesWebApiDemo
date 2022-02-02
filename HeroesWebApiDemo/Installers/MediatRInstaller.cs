using MediatR;

namespace HeroesWebApiDemo.Installers;

public class MediatRInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(typeof(Program));
    }
}