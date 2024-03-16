using Imagine_todo.application.Contracts.Identity;
using Imagine_todo.application.Features.User.Request.Commands;
using MediatR;

namespace Imagine_todo.application.Features.User.Handler.Commands
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUserService _userService;

        public DeleteUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _userService.DeleteUser(request.Id);
            return Unit.Value;
        }
    }
}
