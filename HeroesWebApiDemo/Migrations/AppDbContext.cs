using HeroesWebApiDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeroesWebApiDemo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Hero> Heroes => Set<Hero>();
}