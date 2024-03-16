using Imagine_todo.application.Contracts.Identity;
using Imagine_todo.application.Features.User.Request.Commands;
using Imagine_todo.application.Model.Identity;
using MediatR;

namespace Imagine_todo.application.Features.User.Handler.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, RegistrationResponse>
    {
        private readonly IAuthService _authService;

        public CreateUserHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RegistrationResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _authService.Register(request.userDto);
        }
    }
}
