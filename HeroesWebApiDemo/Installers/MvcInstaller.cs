using FluentValidation.AspNetCore;
using JetBrains.Annotations;

namespace HeroesWebApiDemo.Installers;

[UsedImplicitly]
public class MvcInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers().AddFluentValidation(config => 
            config.RegisterValidatorsFromAssemblyContaining<Program>());
        services.AddEndpointsApiExplorer();
    }
}