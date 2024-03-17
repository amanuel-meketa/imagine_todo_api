using FluentValidation;

namespace Imagine_todo.application.Dtos.Validator
{
    public class UpdateTodoDtoValidator : AbstractValidator<TodoDto>
    {
        public UpdateTodoDtoValidator()
        {
            Include(new TodoDtoValidator());
            RuleFor(v => v.Id).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
