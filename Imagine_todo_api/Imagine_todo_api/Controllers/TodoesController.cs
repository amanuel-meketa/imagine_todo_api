using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Imagine_todo.domain;
using Imagine_todo.Persistence;

namespace Imagine_todo_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TodoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodo()
        {
            return await _context.todos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(Guid id)
        {
            var todo = await _context.todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(Guid id, Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }

            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(Todo todo)
        {
            _context.todos.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodo", new { id = todo.Id }, todo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            var todo = await _context.todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.todos.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoExists(Guid id)
        {
            return _context.todos.Any(e => e.Id == id);
        }
    }
}
