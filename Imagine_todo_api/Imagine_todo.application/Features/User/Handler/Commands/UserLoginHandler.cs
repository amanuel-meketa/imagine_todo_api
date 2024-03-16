using Imagine_todo.application.Contracts.Identity;
using Imagine_todo.application.Features.User.Request.Commands;
using Imagine_todo.application.Model.Identity;
using MediatR;

namespace Imagine_todo.application.Features.User.Handler.Commands
{
    public class UserLoginHandler : IRequestHandler<UserLoginCommand, AuthResponse>
    {
        private readonly IAuthService _authService;

        public UserLoginHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public Task<AuthResponse> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            return _authService.Login(request.authRequest); ;
        }
    }
}
