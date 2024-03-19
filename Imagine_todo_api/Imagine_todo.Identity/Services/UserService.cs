using AutoMapper;
using Imagine_todo.application.Contracts.Identity;
using Imagine_todo.application.Dtos.Identity;
using Imagine_todo.application.Exceptions;
using Imagine_todo.domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Imagine_todo.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<ApplicationUser>> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return users;
        }

        public async Task<ApplicationUser> GetUser(Guid userId)
        {
            var User = await _userManager.FindByIdAsync(userId.ToString());
            if (User == null)
                throw new NotFoundException($"User with ID {userId} could not be found.");

            return User;
        }

        public async Task<bool> UpdateUser(ApplicationUser updatedUser)
        {
            var user = await _userManager.FindByIdAsync(updatedUser.Id.ToString());
            if (user == null)
                throw new NotFoundException($"User with ID {updatedUser.Id} could not be found.");

            user.Email = updatedUser.Email;
            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new NotFoundException($"User with ID {userId} could not be found.");

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
    }
}
