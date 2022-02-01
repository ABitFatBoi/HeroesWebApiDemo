using HeroesWebApiDemo.Migrations;
using HeroesWebApiDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeroesWebApiDemo.Services;

public class HeroService : IHeroService
{
    private readonly AppDbContext _dbContext;
    public HeroService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Hero>> GetAllHeroesAsync()
    {
        return await _dbContext.Heroes.ToListAsync();
    }

    public async Task<Hero?> GetHeroByIdAsync(Guid id)
    {
        return await _dbContext.Heroes.SingleOrDefaultAsync(h => h.Id == id);
    }

    public async Task<bool> CreateHeroAsync(Hero hero)
    {
        await _dbContext.Heroes.AddAsync(hero);
        var created = await _dbContext.SaveChangesAsync();
        return created > 0;
    }

    public async Task<bool> UpdateHeroAsync(Hero hero)
    {
        _dbContext.Heroes.Update(hero);
        var updated = await _dbContext.SaveChangesAsync();
        return updated > 0;
    }

    public async Task<bool> DeleteHeroAsync(Guid id)
    {
        var hero = await GetHeroByIdAsync(id);

        if (hero == null) return false;

        _dbContext.Heroes.Remove(hero);
        var deleted = await _dbContext.SaveChangesAsync();
        return deleted > 0;
    }
}