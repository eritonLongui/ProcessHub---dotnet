using FluentValidation;

namespace ProcessHub.Validators.Process
{
    public class UpdateProcessValidator : AbstractValidator<UpdateProcessDto>
    {
        public UpdateProcessValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty();
        }
    }
}