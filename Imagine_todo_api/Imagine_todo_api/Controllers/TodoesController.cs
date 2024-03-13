using Microsoft.AspNetCore.Mvc;
using MediatR;
using Imagine_todo.application.Features.Todos.Request.Queries;
using Imagine_todo.application.Dtos;
using Imagine_todo.application.Features.Todos.Request.Commands;

namespace Imagine_todo_api.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TodoesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoesController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoListDto>>> Get()
        {
            var response = await _mediator.Send(new GetTodoListRequest());
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<TodoDto> Get(Guid id)
        {
            var detailQuerie = new GetTodoDetailRequest
            {
                Id = id
            };

            return await _mediator.Send(detailQuerie);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody] TodoCreateDto todo)
        {
            var createCommand = new CreateTodoCommand
            {
                todoCreateDto = todo  
            };

            var response = await _mediator.Send(createCommand);
            var locationUri = $"{Request.Scheme}://{Request.Host.ToUriComponent()}/api/Todo/{response}";

            return Created(locationUri, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] TodoDto todo)
        {
            var updateCommand = new UpdateTodoCommand
            {
                todoDto = todo
            };
            await _mediator.Send(updateCommand);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var detailQuerie = new DeleteTodoCommand
            {
                Id = id
            };
            await _mediator.Send(detailQuerie);
            return NoContent();
        }
    }
}
