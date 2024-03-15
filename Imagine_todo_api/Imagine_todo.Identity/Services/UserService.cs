using Imagine_todo.application.Contracts.Identity;
using Imagine_todo.application.Model.Identity;
using Imagine_todo.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Imagine_todo.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> GetUser(Guid userId)
        {
            var User = await _userManager.FindByIdAsync(userId.ToString());
            return new User
            {
                Email = User.Email,
                Id = User.Id,
                Firstname = User.FirstName,
                Lastname = User.LastName
            };
        }

        public async Task<List<User>> GetUsers()
        {
            var Users = await _userManager.GetUsersInRoleAsync("User");
            return Users.Select(q => new User
            {
                Id = q.Id,
                Email = q.Email,
                Firstname = q.FirstName,
                Lastname = q.LastName
            }).ToList();
        }

        public async Task<bool> UpdateUser(User updatedUser)
        {
            var user = await _userManager.FindByIdAsync(updatedUser.Id.ToString());
            if (user == null)
                throw new Exception($"User with ID {updatedUser.Id} could not be found.");

            user.Email = updatedUser.Email;
            user.FirstName = updatedUser.Firstname;
            user.LastName = updatedUser.Lastname;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new Exception($"User with ID {userId} could not be found.");

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
    }
}
