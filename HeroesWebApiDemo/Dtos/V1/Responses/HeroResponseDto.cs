using HeroesWebApiDemo.Enums;

namespace HeroesWebApiDemo.Dtos.V1.Responses;

public class HeroResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsMelee { get; set; }
    public HeroType Type { get; set; }
}