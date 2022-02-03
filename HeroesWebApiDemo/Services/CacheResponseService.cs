namespace HeroesWebApiDemo.Services;

public class CacheResponseService : ICacheResponseService
{
    public async Task CacheResponseAsync(string key, object response, int expirationTimeInSeconds)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetCachedResponseAsync(string key)
    {
        throw new NotImplementedException();
    }
}