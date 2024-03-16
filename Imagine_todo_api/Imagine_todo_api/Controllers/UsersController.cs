using Microsoft.AspNetCore.Mvc;
using Imagine_todo.application.Model.Identity;
using Imagine_todo.application.Dtos.Identity;
using Imagine_todo.application.Features.User.Request.Commands;
using Imagine_todo.application.Features.User.Request.Queries;
using MediatR;

namespace Imagine_todo_api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController( IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            var loginQuerie = new UserLoginCommand { authRequest = request };
            return Ok(await _mediator.Send(loginQuerie));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            var detailQuerie = new CreateUserCommand { userDto = request };
            var response = await _mediator.Send(request);
            var locationUri = $"{Request.Scheme}://{Request.Host.ToUriComponent()}/api/todos/{response}";

            return Created(locationUri, response);
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetUserListRequest()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<UserDto>>> Get(Guid id)
        {
            var detailQuerie = new GetUserDetailRequest { Id = id };
            return Ok(await _mediator.Send(detailQuerie));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> update(UserDto request)
        {
            var updateQuerie = new UpdateUserCommand { UserDto = request };
            await _mediator.Send(updateQuerie);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteQuerie = new DeleteUserCommand { Id = id };
            await _mediator.Send(deleteQuerie);
            return NoContent();
        }

        [HttpGet("current-user")]
        public async Task<ActionResult<UserDto>> GetCurrentLoggedInUser()
        {
            var user = HttpContext.User;

            if (!user.Identity.IsAuthenticated)
                return Unauthorized("No user authenticated.");

            var userIdClaim = user.FindFirst("uid");

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userGuid))
                return BadRequest("Invalid user ID claim.");

            var detailQuerie = new GetUserDetailRequest { Id = userGuid };

            var userProfile = await _mediator.Send(detailQuerie);

            if (userProfile == null)
                return NotFound("User not found.");

            return Ok(userProfile);
        }
    }
}
