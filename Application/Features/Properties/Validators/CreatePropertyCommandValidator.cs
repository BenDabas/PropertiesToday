using Application.Features.Properties.Commands;
using FluentValidation;

namespace Application.Features.Properties.Validators
{
    public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
    {
        public CreatePropertyCommandValidator()
        {
            // Create nested validator.
            RuleFor(request => request.PropertyRequest)
                .SetValidator(new NewPropertyValidator());
        }
    }
}
