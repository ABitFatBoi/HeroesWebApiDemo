using JetBrains.Annotations;

namespace HeroesWebApiDemo.Options;

public class RedisOptions
{
    [UsedImplicitly]
    public bool Enabled { get; set; }
    [UsedImplicitly]
    public string ConnectionString { get; set; } = default!;
}