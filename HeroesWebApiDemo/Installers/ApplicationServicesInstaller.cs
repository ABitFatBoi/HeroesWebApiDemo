using HeroesWebApiDemo.Services;
using JetBrains.Annotations;

namespace HeroesWebApiDemo.Installers;

[UsedImplicitly]
public class ApplicationServicesInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IHeroService, HeroService>();
        services.AddScoped<IIdentityService, IdentityService>();
    }
}