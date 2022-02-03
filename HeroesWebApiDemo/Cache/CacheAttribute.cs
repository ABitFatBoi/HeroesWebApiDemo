using System.Text;
using HeroesWebApiDemo.Options;
using HeroesWebApiDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HeroesWebApiDemo.Cache;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CacheAttribute : Attribute, IAsyncActionFilter
{
    private readonly int _expirationTimeInSeconds;

    public CacheAttribute(int expirationTimeInSeconds)
    {
        _expirationTimeInSeconds = expirationTimeInSeconds;
    }
    
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var options = context.HttpContext.RequestServices.GetRequiredService<RedisOptions>();

        if (!options.Enabled)
        {
            await next();
            return;
        }

        var cacheResponseService = context.HttpContext.RequestServices.GetRequiredService<ICacheResponseService>();
        var cacheKey = GenerateCacheKey(context.HttpContext.Request);
        var cachedResponse = await cacheResponseService.GetCachedResponseAsync(cacheKey);
        
        if (!string.IsNullOrEmpty(cachedResponse))
        {
            var contentResult = new ContentResult
            {
                Content = cachedResponse,
                StatusCode = 200,
                ContentType = "application/json"
            };
            context.Result = contentResult;
            return;
        }

        var executedContext = await next();

        if (executedContext.Result is OkObjectResult okObjectResult)
        {
            await cacheResponseService.CacheResponseAsync(
                cacheKey, okObjectResult.Value!, _expirationTimeInSeconds);
        }
    }

    private string GenerateCacheKey(HttpRequest request)
    {
        var sb = new StringBuilder();
        sb.Append(request.Path);
        
        foreach (var keyValuePair in request.Query.OrderBy(x => x.Key))
        {
            sb.Append($"&{keyValuePair.Key}={keyValuePair.Value}");
        }

        return sb.ToString();
    }
}