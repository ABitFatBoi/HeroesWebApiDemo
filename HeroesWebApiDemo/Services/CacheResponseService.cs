using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace HeroesWebApiDemo.Services;

public class CacheResponseService : ICacheResponseService
{
    private readonly IDistributedCache _distributedCache;

    public CacheResponseService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }
    
    public async Task CacheResponseAsync(string key, object? response, int expirationTimeInSeconds)
    {
        if (response is null) return;

        var serializedResponse = JsonConvert.SerializeObject(response);

        await _distributedCache.SetStringAsync(key, serializedResponse, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(expirationTimeInSeconds)
        });
    }

    public async Task<string?> GetCachedResponseAsync(string key)
    {
        var cachedResponse = await _distributedCache.GetStringAsync(key);
        
        return string.IsNullOrEmpty(cachedResponse) ? null : cachedResponse;
    }
}