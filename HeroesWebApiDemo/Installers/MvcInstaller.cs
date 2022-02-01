using FluentValidation.AspNetCore;
using HeroesWebApiDemo.Filters;
using JetBrains.Annotations;

namespace HeroesWebApiDemo.Installers;

[UsedImplicitly]
public class MvcInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(options => options.Filters.Add<ValidationFilter>())
            .AddFluentValidation(config => 
            config.RegisterValidatorsFromAssemblyContaining<Program>());
        services.AddEndpointsApiExplorer();
    }
}