using System.ComponentModel.DataAnnotations;
using HeroesWebApiDemo.Enums;

namespace HeroesWebApiDemo.Entities;

public class Hero
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public bool IsMelee { get; set; }
    [Required]
    public HeroType Type { get; set; }
}