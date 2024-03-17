using AutoMapper;
using Imagine_todo.application.Contracts.Identity;
using Imagine_todo.application.Dtos.Identity;
using Imagine_todo.application.Features.User.Request.Queries;
using MediatR;

namespace Imagine_todo.application.Features.User.Handler.Queries
{
    public class GetUserDetailHandler : IRequestHandler<GetUserDetailRequest, UserDto>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetUserDetailHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserDetailRequest request, CancellationToken cancellationToken)
        {
           var response = await _userService.GetUser(request.Id);

            return _mapper.Map<UserDto>(response);
        }
    }
}
