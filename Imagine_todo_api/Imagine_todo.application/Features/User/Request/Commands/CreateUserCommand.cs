using Imagine_todo.application.Dtos.Identity;
using Imagine_todo.application.Model.Identity;
using MediatR;

namespace Imagine_todo.application.Features.User.Request.Commands
{
    public class CreateUserCommand : IRequest<RegistrationResponse>
    {
        public CreatUserDto? userDto {  get; set; }
    }
}
