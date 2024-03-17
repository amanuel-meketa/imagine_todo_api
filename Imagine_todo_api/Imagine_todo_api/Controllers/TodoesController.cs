using Microsoft.AspNetCore.Mvc;
using MediatR;
using Imagine_todo.application.Features.Todos.Request.Queries;
using Imagine_todo.application.Dtos;
using Imagine_todo.application.Features.Todos.Request.Commands;
using Microsoft.AspNetCore.Authorization;

namespace Imagine_todo_api.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    [Authorize]
    public class TodoesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoesController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<List<TodoListDto>>> Get()
        {
            var response = await _mediator.Send(new GetTodoListRequest());
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<TodoDto> Get(Guid id)
        {
            var detailQuerie = new GetTodoDetailRequest { Id = id };

            return await _mediator.Send(detailQuerie);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody] TodoCreateDto todo)
        {
            var createCommand = new CreateTodoCommand { todoCreateDto = todo };
            var response = await _mediator.Send(createCommand);
            var locationUri = $"{Request.Scheme}://{Request.Host.ToUriComponent()}/api/todos/{response}";

            return Created(locationUri, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] TodoDto todo)
        {
            var updateCommand = new UpdateTodoCommand { todoDto = todo };
            await _mediator.Send(updateCommand);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var detailQuerie = new DeleteTodoCommand { Id = id };
            await _mediator.Send(detailQuerie);

            return NoContent();
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<TodoListDto>>> GetFilteredTasks([FromQuery] DateTime? dueDate, [FromQuery] string status)
        {
            var todos = await _mediator.Send(new GetTodoListRequest());

            if (dueDate.HasValue)
                todos = todos.Where(todo => todo.DueDate.Date == dueDate.Value.Date).ToList();

            if (!string.IsNullOrEmpty(status))
                todos = todos.Where(todo => todo.Status == status).ToList();

            return Ok(todos);
        }

        [HttpPatch("assign-task")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> AssignTask(Guid todoId, Guid userId)
        {
           var assignQuerie = new AssignTaskCommand
           { 
              TodoId = todoId ,
              UserId = userId
            };

           await _mediator.Send(assignQuerie);
            return NoContent();
        }

        [HttpPatch("my-task")]
        public async Task<ActionResult<List<TodoDto>>> Myasks()
        {
            var user = HttpContext.User;

            if (!user.Identity.IsAuthenticated)
                return Unauthorized("No user authenticated.");

            var userIdClaim = HttpContext.User.FindFirst("uid");

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userGuid))
                return BadRequest("Invalid user ID claim.");

            return Ok(await _mediator.Send(new GetMyTodoRequest { Id = userGuid }));
        }
    }
}
