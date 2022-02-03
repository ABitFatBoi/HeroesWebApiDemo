using HeroesWebApiDemo.Options;
using HeroesWebApiDemo.Services;
using JetBrains.Annotations;

namespace HeroesWebApiDemo.Installers;

[UsedImplicitly]
public class RedisCacheInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var options = new RedisOptions();
        configuration.GetSection(nameof(RedisOptions)).Bind(options);
        services.AddSingleton(options);
        
        if (!options.Enabled) return;

        services.AddStackExchangeRedisCache(opt => opt.Configuration = options.ConnectionString);
        services.AddSingleton<ICacheResponseService, CacheResponseService>();
    }
}