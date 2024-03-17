using MediatR;

namespace Imagine_todo.application.Features.Todos.Request.Commands
{
    public class AssignTaskCommand : IRequest<Unit>
    {
        public Guid TodoId { get; set; }
        public Guid UserId { get; set; }
    }
}
