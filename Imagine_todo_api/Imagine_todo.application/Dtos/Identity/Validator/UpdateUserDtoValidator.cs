using FluentValidation;

namespace Imagine_todo.application.Dtos.Identity.Validator
{
    public class UpdateUserDtoValidator : AbstractValidator<UserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(v => v.FirstName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .MaximumLength(32).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            RuleFor(v => v.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(32).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            RuleFor(v => v.UserName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(32).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            RuleFor(v => v.Id).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
