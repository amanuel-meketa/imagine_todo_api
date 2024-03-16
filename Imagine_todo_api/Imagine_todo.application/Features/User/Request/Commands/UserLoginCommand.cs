using Imagine_todo.application.Model.Identity;
using MediatR;

namespace Imagine_todo.application.Features.User.Request.Commands
{
    public class UserLoginCommand : IRequest<AuthResponse>
    {
        public AuthRequest? authRequest { get; set; }
    }
}
