using FluentValidation;

namespace Imagine_todo.application.Dtos.Validator
{
    public class ITodoDtoValidator : AbstractValidator<ITodoDto>
    {
        public ITodoDtoValidator()
        {
            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(20).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");


            RuleFor(v => v.DueDate)
                .GreaterThan(DateTime.Today).WithMessage("{PropertyName} must be greater than the current date.");
        }
    }
}
