using FluentValidation;
using Services.Services.Models.Request;

namespace Services.Validators;

public class GetStatusValidator : AbstractValidator<GetStatusModel>
{
    public GetStatusValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}