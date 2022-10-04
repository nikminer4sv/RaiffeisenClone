using FluentValidation;

namespace RaiffeisenClone.Application.ViewModels.IdViewModel;

public class IdViewModelValidator : AbstractValidator<IdViewModel>
{
    public IdViewModelValidator()
    {
        RuleFor(model => model.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Id is null");
    }
}