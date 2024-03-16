using MediatR;

namespace Imagine_todo.application.Features.User.Request.Commands
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
