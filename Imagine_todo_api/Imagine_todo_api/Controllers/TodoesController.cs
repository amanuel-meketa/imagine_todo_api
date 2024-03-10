using Microsoft.AspNetCore.Mvc;
using Imagine_todo.domain;
using Imagine_todo.application.Contracts.Persistence;

namespace Imagine_todo_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoesController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        public TodoesController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Todo>>> Get()
        {
            return Ok(await _todoRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<Todo> Get(Guid id)
        {
            return await _todoRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody] Todo todo)
        {
            var response = await _todoRepository.Add(todo);
            var locationUri = $"{Request.Scheme}://{Request.Host.ToUriComponent()}/api/Todo/{response}";

            return Created(locationUri, response);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] Todo todo)
        {
            await _todoRepository.Update(todo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Todo entity)
        {
            await _todoRepository.Delete(entity);
            return NoContent();
        }
    }
}
