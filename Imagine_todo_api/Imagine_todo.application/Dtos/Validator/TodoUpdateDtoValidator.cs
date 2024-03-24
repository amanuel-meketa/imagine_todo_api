using FluentValidation;

namespace Imagine_todo.application.Dtos.Validator
{
    public class TodoUpdateDtoValidator : AbstractValidator<TodoUpdateDto>
    {
        public TodoUpdateDtoValidator()
        {
            Include(new TodoDtoValidator());
        }
    }
}
