using HeroesWebApiDemo.Dtos.V1.Responses;
using HeroesWebApiDemo.Entities;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HeroesWebApiDemo.Filters;

[UsedImplicitly]
public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errorsInModel = context.ModelState
                .Where(p => p.Value?.Errors.Count > 0)
                .ToDictionary(p => p.Key,
                    p => p.Value?.Errors.Select(e => e.ErrorMessage))
                .ToList();

            var validationErrorResponse = new ValidationErrorResponse
            {
                Errors = errorsInModel.Select(pair =>
                    {
                        return new ValidationError
                        {
                            FieldName = pair.Key,
                            ErrorMessages = pair.Value!.ToList()
                        };
                    }
                ).ToList()
            };

            context.Result = new BadRequestObjectResult(validationErrorResponse);
            return;
        }
        
        await next();
    }
}