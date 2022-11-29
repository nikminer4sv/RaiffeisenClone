using FluentValidation;

namespace Identity.Application.ViewModels.RegisterViewModel;

public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
{

    public RegisterViewModelValidator()
    {
        RuleFor(model => model.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Password is empty")
            .MinimumLength(8)
            .WithMessage("Minimum password length must be 8");
        
        RuleFor(model => model.FirstName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Name is empty")
            .Length(6,20)
            .WithMessage("Name length must be between 6 and 20");
        
        RuleFor(model => model.LastName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Last name is empty")
            .Length(6,20)
            .WithMessage("Last name length must be between 6 and 20");
        
        RuleFor(model => model.Username)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Username is empty")
            .Length(6,20)
            .WithMessage("Username length must be between 6 and 20");

        RuleFor(model => model.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Email is empty")
            .EmailAddress()
            .WithMessage("Invalid email");

        RuleFor(model => model.DateOfBirth)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Date is empty")
            .LessThan(DateTime.Now)
            .WithMessage("Invalid date");
    }
}