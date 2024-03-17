using Imagine_todo.application.Dtos.Identity;
using Imagine_todo.application.Model.Identity;

namespace Imagine_todo.application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<RegistrationResponse> Register(CreatUserDto request);

    }
}
