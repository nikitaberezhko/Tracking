using FluentValidation;
using Services.Services.Models.Request;

namespace Services.Validators;

public class GetStatusByOrderIdValidator : AbstractValidator<GetStatusByOrderIdModel>
{
    public GetStatusByOrderIdValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}