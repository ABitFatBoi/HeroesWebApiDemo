using FluentValidation;
using HeroesWebApiDemo.Dtos.V1.Requests;

namespace HeroesWebApiDemo.Validators;

public class HeroUpdateDtoValidator : AbstractValidator<HeroUpdateDto>
{
    public HeroUpdateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Matches("^[a-zA-Z0-9 ()]*$");

        RuleFor(x => x.Type)
            .IsInEnum();
    }
}