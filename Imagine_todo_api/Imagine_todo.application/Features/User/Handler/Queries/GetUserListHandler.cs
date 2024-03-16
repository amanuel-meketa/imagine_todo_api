using AutoMapper;
using Imagine_todo.application.Contracts.Identity;
using Imagine_todo.application.Dtos.Identity;
using Imagine_todo.application.Features.User.Request.Queries;
using MediatR;

namespace Imagine_todo.application.Features.User.Handler.Queries
{
    public class GetUserListHandler : IRequestHandler<GetUserListRequest, List<UserDto>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetUserListHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetUserListRequest request, CancellationToken cancellationToken)
        {
            var response = await _userService.GetUsers();
            return _mapper.Map<List<UserDto>>(response);
        }
    }
}
