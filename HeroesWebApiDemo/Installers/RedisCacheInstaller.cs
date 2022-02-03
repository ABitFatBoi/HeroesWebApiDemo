using HeroesWebApiDemo.Options;

namespace HeroesWebApiDemo.Installers;

public class RedisCacheInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var options = new RedisOptions();
        configuration.GetSection(nameof(RedisOptions)).Bind(options);
        
        if (!options.Enabled) return;

        services.AddStackExchangeRedisCache(opt => opt.Configuration = options.ConnectionString);
    }
}