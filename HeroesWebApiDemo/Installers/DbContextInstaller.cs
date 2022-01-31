using HeroesWebApiDemo.Data;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace HeroesWebApiDemo.Installers;

[UsedImplicitly]
public class DbContextInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("SqlServerConnection");

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
    }
}