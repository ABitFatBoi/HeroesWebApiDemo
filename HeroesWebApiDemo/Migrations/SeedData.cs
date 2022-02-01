using HeroesWebApiDemo.Migrations;
using HeroesWebApiDemo.Entities;
using HeroesWebApiDemo.Enums;
using Microsoft.EntityFrameworkCore;

namespace HeroesWebApiDemo.Migrations;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var dbContext = new AppDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());
        
        if (dbContext.Heroes.Any())
        {
            return;
        }

        PopulateTestData(dbContext);
    }

    public static void PopulateTestData(AppDbContext dbContext)
    {
        foreach (var item in dbContext.Heroes)
        {
            dbContext.Remove(item);
        }

        dbContext.SaveChanges();

        var heroes = new Hero[5]
        {
            new Hero
            {
                Id = new Guid("10effdd4-8036-4853-baee-8f5dbfc6ee75"),
                Name = "Hero 1 (Ranged Mage)",
                IsMelee = false,
                Type = HeroType.Mage
            },
            new Hero
            {
                Id = new Guid("af3766b3-84ae-4b44-b5e0-c29eae0dc6d9"),
                Name = "Hero 2 (Ranged Healer)",
                IsMelee = false,
                Type = HeroType.Healer
            },
            new Hero
            {
                Id = new Guid("6befaf14-02be-4d52-a133-e0ff86fbc4ff"),
                Name = "Hero 3 (Melee Warrior)",
                IsMelee = true,
                Type = HeroType.Warrior
            },
            new Hero
            {
                Id = new Guid("9e3176ed-e586-4671-9cd3-3de69f38b5a5"),
                Name = "Hero 4 (Melee Paladin)",
                IsMelee = true,
                Type = HeroType.Paladin
            },
            new Hero
            {
                Id = new Guid("60469803-eb93-423f-8379-a019d17f4717"),
                Name = "Hero 5 (Ranged Mage)",
                IsMelee = false,
                Type = HeroType.Mage
            }
        };

        dbContext.Heroes.AddRange(heroes);

        dbContext.SaveChanges();
    }
}