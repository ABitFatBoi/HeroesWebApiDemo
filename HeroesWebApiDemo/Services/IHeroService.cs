using HeroesWebApiDemo.Entities;

namespace HeroesWebApiDemo.Services;

public interface IHeroService
{
    Task<List<Hero>> GetAllHeroesAsync();
    Task<Hero?> GetHeroByIdAsync(Guid id);
    Task<bool> CreateHeroAsync(Hero hero);
    Task<bool> UpdateHeroAsync(Hero hero);
    Task<bool> DeleteHeroAsync(Guid id);
}