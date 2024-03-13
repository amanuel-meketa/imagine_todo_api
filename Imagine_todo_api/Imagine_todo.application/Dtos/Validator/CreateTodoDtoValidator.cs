using FluentValidation;

namespace Imagine_todo.application.Dtos.Validator
{
    public class CreateTodoDtoValidator : AbstractValidator<TodoCreateDto>
    {
        public CreateTodoDtoValidator()
        {
            Include(new ITodoDtoValidator());
        }
    }
}
