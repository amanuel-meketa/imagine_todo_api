using MediatR;

namespace Imagine_todo.application.Features.Todos.Request.Commands
{
    public class DeleteTodoCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
