using Imagine_todo.application.Dtos;
using MediatR;

namespace Imagine_todo.application.Features.Todos.Request.Commands
{
    public class CreateTodoCommand : IRequest<TodoCreateResponseDto>
    {
        public TodoCreateDto? todoCreateDto { get; set; }
    }
}
