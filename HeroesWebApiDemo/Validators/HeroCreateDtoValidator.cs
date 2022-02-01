using FluentValidation;
using HeroesWebApiDemo.Dtos.V1.Requests;

namespace HeroesWebApiDemo.Validators;

public class HeroCreateDtoValidator : AbstractValidator<HeroCreateDto>
{
    public HeroCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Matches("^[a-zA-Z0-9 ()]*$");

        RuleFor(x => x.Type)
            .IsInEnum();
    }
}