using Imagine_todo.application.Dtos.Identity;
using Imagine_todo.domain;

namespace Imagine_todo.application.Contracts.Identity
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsers();
        Task<UserDto> GetUser(Guid userId);
        Task<bool> UpdateUser(UserDto updatedUser);
        Task<bool> DeleteUser(Guid userId);
    }
}
