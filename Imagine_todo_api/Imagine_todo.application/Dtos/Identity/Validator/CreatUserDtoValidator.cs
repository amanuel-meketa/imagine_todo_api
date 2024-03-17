using FluentValidation;
using System.Text.RegularExpressions;

namespace Imagine_todo.application.Dtos.Identity.Validator
{
    public class CreatUserDtoValidator : AbstractValidator<CreatUserDto>
    {
        public CreatUserDtoValidator()
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

            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(v => v.Password)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MinimumLength(8).WithMessage("{PropertyName} must be at least {MinLength} characters long.")
                .Must(BeStrongPassword).WithMessage("{PropertyName} must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");

        }
        private bool BeStrongPassword(string password)
        {
            // Regular expression to match strong password criteria: at least one uppercase, one lowercase, one digit, and one special character
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            return regex.IsMatch(password);
        }
    }
}
