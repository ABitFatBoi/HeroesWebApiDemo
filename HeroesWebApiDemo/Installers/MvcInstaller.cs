using JetBrains.Annotations;

namespace HeroesWebApiDemo.Installers;

[UsedImplicitly]
public class MvcInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
    }
}