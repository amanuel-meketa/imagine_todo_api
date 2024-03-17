using Imagine_todo.application.Contracts.Identity;
using Imagine_todo.application.Features.User.Request.Commands;
using Imagine_todo.application.Model.Identity;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Imagine_todo.application.Dtos.Identity.Validator;
using Imagine_todo.application.Dtos.Identity;

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
            await ValidateUserCreateDtoAsync(request.userDto);

            return await _authService.Register(request.userDto);
        }

        private async Task ValidateUserCreateDtoAsync(CreatUserDto todoCreateDto)
        {
            var validator = new CreatUserDtoValidator();
            var validatorResult = await validator.ValidateAsync(todoCreateDto);

            if (!validatorResult.IsValid)
            {
                var errors = validatorResult.Errors.Select(e => e.ErrorMessage);
                var errorMessage = string.Join(Environment.NewLine, errors);
                throw new ValidationException(errorMessage);
            }
        }
    }
}
