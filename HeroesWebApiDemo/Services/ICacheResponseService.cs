namespace HeroesWebApiDemo.Services;

public interface ICacheResponseService
{
    Task CacheResponseAsync(string key, object response, int expirationTimeInSeconds);
    Task<string> GetCachedResponseAsync(string key);
}