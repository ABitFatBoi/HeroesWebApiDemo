namespace HeroesWebApiDemo.Dtos.Responses;

public class AuthenticationFailedDto
{
    public IEnumerable<string> ErrorMessages { get; set; }
}