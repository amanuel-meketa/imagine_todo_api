using AutoMapper;
using Imagine_todo.application.Contracts.Identity;
using Imagine_todo.application.Dtos.Identity.Validator;
using Imagine_todo.application.Features.User.Request.Commands;
using MediatR;
using Imagine_todo.application.Dtos.Identity;

namespace Imagine_todo.application.Features.User.Handler.Commands
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await ValidateTodoUpdateDtoAsync(request.UserDto);
            var response = await _userService.GetUser(request.UserDto.Id);

            _mapper.Map(request.UserDto, response);
            await _userService.UpdateUser(response);

            return Unit.Value;
        }

        private async Task ValidateTodoUpdateDtoAsync(UserDto todoDto)
        {
            var validator = new UpdateUserDtoValidator();
            var validatorResult = await validator.ValidateAsync(todoDto);

            if (!validatorResult.IsValid)
            {
                var errors = validatorResult.Errors.Select(e => e.ErrorMessage);
                var errorMessage = string.Join(Environment.NewLine, errors);
                throw new System.ComponentModel.DataAnnotations.ValidationException(errorMessage);
            }
        }
    }
}
