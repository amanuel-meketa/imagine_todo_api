using MediatR;

namespace Imagine_todo.application.Features.Todos.Request.Commands
{
    public class CompleteTaskCommand : IRequest<Unit>
    {
        public Guid ID { get; set; }
        public string? Status { get; set; }
    }
}
