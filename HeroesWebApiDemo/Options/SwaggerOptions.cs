using JetBrains.Annotations;

namespace HeroesWebApiDemo.Options;

public class SwaggerOptions
{
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public string JsonRoute { get; set; } = default!;
    
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public string Description { get; set; } = default!;
    
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public string UiEndpoint { get; set; } = default!;
}