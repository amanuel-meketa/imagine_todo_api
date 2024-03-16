using Imagine_todo.application.Dtos.Identity;
using MediatR;

namespace Imagine_todo.application.Features.User.Request.Queries
{
    public class GetUserDetailRequest : IRequest<UserDto>
    {
        public Guid Id { get; set; }
    }
}
