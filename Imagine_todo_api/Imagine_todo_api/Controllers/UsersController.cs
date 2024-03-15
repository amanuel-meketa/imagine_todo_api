using Microsoft.AspNetCore.Mvc;
using Imagine_todo.application.Contracts.Identity;
using Imagine_todo.application.Model.Identity;

namespace Imagine_todo_api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public UsersController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            return Ok(await _authService.Login(request));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            return Ok(await _authService.Register(request));
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            return Ok(await _userService.GetUsers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<User>>> Get(Guid userId)
        {
            return Ok(await _userService.GetUser(userId));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> update(User request)
        {
            await _userService.UpdateUser(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
