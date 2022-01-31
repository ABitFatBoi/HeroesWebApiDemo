namespace HeroesWebApiDemo.Dtos.V1.Responses;

public class AuthenticationFailedDto
{
    public IEnumerable<string> ErrorMessages { get; set; }
}