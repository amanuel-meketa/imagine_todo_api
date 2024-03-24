using Microsoft.AspNetCore.Mvc;
using MediatR;
using Imagine_todo.application.Features.Todos.Request.Queries;
using Imagine_todo.application.Dtos;
using Imagine_todo.application.Features.Todos.Request.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;

namespace Imagine_todo_api.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    [EnableRateLimiting("FixedPolicy")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
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
        public async Task<ActionResult<TodoCreateResponseDto>> Post([FromBody] TodoCreateDto task)
        {
            var createCommand = new CreateTodoCommand { todoCreateDto = task };
            var response = await _mediator.Send(createCommand);
            var locationUri = $"{Request.Scheme}://{Request.Host.ToUriComponent()}/api/tasks/{response}";

            return Created(locationUri, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] TodoUpdateDto task)
        {
            var updateCommand = new UpdateTodoCommand { Id = id, todoDto = task };
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
            var tasks = await _mediator.Send(new GetTodoListRequest());

            if (dueDate.HasValue)
                tasks = tasks.Where(todo => todo.DueDate.Date == dueDate.Value.Date).ToList();

            if (!string.IsNullOrEmpty(status))
                tasks = tasks.Where(todo => todo.Status == status).ToList();

            return Ok(tasks);
        }

        [HttpPatch("assign-task")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> AssignTask(Guid taskId, Guid userId)
        {
           var assignQuerie = new AssignTaskCommand
           { 
              TodoId = taskId,
              UserId = userId
            };

           await _mediator.Send(assignQuerie);
            return NoContent();
        }

        [HttpGet("my-task")]
        [Authorize]
        public async Task<ActionResult<List<TodoDto>>> Myasks()
        {
            var userId = GetUserIdFromCookie();

            if (userId == Guid.Empty)
                return Unauthorized("No user authenticated.");

            var request = new GetMyTodoRequest { Id = userId };
            var tasks = await _mediator.Send(request);

            return Ok(tasks);
        }

        [HttpPatch("{id}/complete")]
        [Authorize]
        public async Task<ActionResult> CompleteTask(Guid id, [FromBody] string status)
        {
            var approveCommand = new CompleteTaskCommand { ID = id, Status = status };
            await _mediator.Send(approveCommand);

            return NoContent();
        }

        private Guid GetUserIdFromCookie()
        {
            var user = HttpContext.User;

            if (!user.Identity.IsAuthenticated)
                return Guid.Empty;

            var userIdClaim = HttpContext.User.FindFirst("uid");

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                return Guid.Empty;

            return userId;
        }
    }
}
