using JetBrains.Annotations;

namespace HeroesWebApiDemo.Installers;

[UsedImplicitly]
public class AutoMapperInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}