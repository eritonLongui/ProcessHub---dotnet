using FluentValidation;

namespace ProcessHub.Validators.Process
{
    public class CreateProcessValidator : AbstractValidator<CreateProcessDto>
    {
        public CreateProcessValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required");

            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage("ClientId is required");
        }
    }
}