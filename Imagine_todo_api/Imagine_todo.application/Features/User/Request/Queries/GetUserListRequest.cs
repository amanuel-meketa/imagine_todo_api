using Imagine_todo.application.Dtos.Identity;
using MediatR;

namespace Imagine_todo.application.Features.User.Request.Queries
{
    public class GetUserListRequest : IRequest<List<UserDto>>
    {
    }
}
