using Application.Models.Images;
using FluentValidation;

namespace Application.Features.Images.Validators
{
    public class NewImageValidator : AbstractValidator<NewImage>
    {
        public NewImageValidator()
        {
            RuleFor(ni => ni.PropertyId)
                .NotEmpty().WithMessage("Property id is required.");

            RuleFor(np => np.Name)
                .MaximumLength(15).WithMessage("Name should not exceed 15 charactors.");
        }
    }
}
