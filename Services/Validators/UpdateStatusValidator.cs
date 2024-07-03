using FluentValidation;
using Services.Services.Models.Request;

namespace Services.Validators;

public class UpdateStatusValidator : AbstractValidator<UpdateStatusModel>
{
    public UpdateStatusValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
        
        RuleFor(x => x.CompletionPercent).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);

        RuleFor(x => x.StatusType).IsInEnum();
    }
}