using HeroesWebApiDemo.Entities;

namespace HeroesWebApiDemo.Dtos.V1.Responses;

public class ValidationErrorResponse
{
    public List<ValidationError> Errors { get; set; } = new();
}