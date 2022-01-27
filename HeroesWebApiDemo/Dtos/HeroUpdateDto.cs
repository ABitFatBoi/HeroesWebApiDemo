using HeroesWebApiDemo.Enums;

namespace HeroesWebApiDemo.Dtos;

public class HeroUpdateDto
{
    public string Name { get; set; }
    public bool IsMelee { get; set; }
    public HeroType Type { get; set; }
}