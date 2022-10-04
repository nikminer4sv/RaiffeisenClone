using FluentValidation;
using RaiffeisenClone.Application.ViewModels;

namespace RaiffeisenClone.Application.Validators;

public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
{
    public LoginViewModelValidator()
    {
        RuleFor(model => model.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Password is empty")
            .MinimumLength(8)
            .WithMessage("Minimum password length must be 8");
        
        RuleFor(model => model.Username)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Username is empty")
            .Length(6,20)
            .WithMessage("Username length must be between 6 and 20");
    }
}