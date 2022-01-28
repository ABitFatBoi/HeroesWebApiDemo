namespace HeroesWebApiDemo.Entities;

public class AuthenticationResult
{
    public string? Token { get; set; }

    public IEnumerable<string>? ErrorMessages { get; set; }
}