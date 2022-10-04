using FluentValidation;
using RaiffeisenClone.Application.ViewModels;

namespace RaiffeisenClone.Application.Validators;

public class DepositViewModelValidator : AbstractValidator<DepositViewModel>
{
    public DepositViewModelValidator()
    {
        RuleFor(model => model.Currency)
            .NotEmpty()
            .WithMessage("Currency is empty")
            .MaximumLength(3)
            .WithMessage("Length must be less or equal to three");

        RuleFor(model => model.IsReplenished)
            .NotEmpty()
            .WithMessage("Is replenished value is empty");
        
        RuleFor(model => model.IsWithdrawed)
            .NotEmpty()
            .WithMessage("Is withdrawed value is empty");
        
        RuleFor(model => model.Bid)
            .NotEmpty()
            .WithMessage("Bid is empty");
        
        RuleFor(model => model.Term)
            .NotEmpty()
            .WithMessage("Term is empty");
    }
}