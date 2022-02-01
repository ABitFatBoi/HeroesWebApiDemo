using FluentValidation;
using HeroesWebApiDemo.Dtos.V1.Requests;

namespace HeroesWebApiDemo.Validators;

public class UserRegistrationDtoValidator : AbstractValidator<UserRegistrationDto>
{
    public UserRegistrationDtoValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress();

        RuleFor(x => x.UserName)
            .Matches("^[a-zA-Z0-9 ]*$");
    }
}