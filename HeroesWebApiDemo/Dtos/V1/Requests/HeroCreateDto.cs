using HeroesWebApiDemo.Enums;

namespace HeroesWebApiDemo.Dtos.V1.Requests;

public class HeroCreateDto
{
    public string Name { get; set; }
    public bool IsMelee { get; set; }
    public HeroType Type { get; set; }
}