using Imagine_todo.application.Dtos.Identity;
using Imagine_todo.domain;

namespace Imagine_todo.application.Contracts.Identity
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetUsers();
        Task<ApplicationUser> GetUser(Guid userId);
        Task<bool> UpdateUser(ApplicationUser updatedUser);
        Task<bool> DeleteUser(Guid userId);
    }
}
