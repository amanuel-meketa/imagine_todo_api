using AutoMapper;
using Imagine_todo.application.Contracts.Identity;
using Imagine_todo.application.Exceptions;
using Imagine_todo.application.Features.User.Request.Commands;
using MediatR;

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
            var response = await _userService.GetUser(request.UserDto.Id);
            if (response == null)
                throw new NotFoundException("Item could not be found.");

            _mapper.Map(request.UserDto, response);
            await _userService.UpdateUser(response);

            return Unit.Value;
        }
    }
}
