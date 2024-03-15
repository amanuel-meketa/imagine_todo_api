using Imagine_todo.application.Model.Identity;

namespace Imagine_todo.application.Contracts.Identity
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(Guid userId);
        Task<bool> UpdateUser(User updatedUser);
        Task<bool> DeleteUser(Guid userId);
    }
}
