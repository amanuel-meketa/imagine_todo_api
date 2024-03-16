using Imagine_todo.application.Dtos.Identity;
using MediatR;

namespace Imagine_todo.application.Features.User.Request.Commands
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public UserDto? UserDto { get; set; }
    }
}
