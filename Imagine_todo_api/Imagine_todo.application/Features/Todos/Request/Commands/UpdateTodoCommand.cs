using Imagine_todo.application.Dtos;
using MediatR;

namespace Imagine_todo.application.Features.Todos.Request.Commands
{
    public class UpdateTodoCommand: IRequest<Unit>
    {
        public TodoDto? todoDto { get; set; }
    }
}
