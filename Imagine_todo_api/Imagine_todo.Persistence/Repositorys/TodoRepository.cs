using Imagine_todo.application.Contracts.Persistence;
using Imagine_todo.application.Exceptions;
using Imagine_todo.domain;
using Imagine_todo.Identity;

namespace Imagine_todo.Persistence.Repositorys
{
    public class TodoRepository : GenericRepository<Todo>, ITodoRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly TodoIdentityDbContext _IdentityDbContext;

        public TodoRepository(ApplicationDbContext dbContext, TodoIdentityDbContext IdentityDbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _IdentityDbContext = IdentityDbContext;
        }

        public async Task AssignTask(Guid taskId, Guid userId)
        {
            var todo = await _dbContext.todos.FindAsync(taskId);

            if (todo == null)
                throw new NotFoundException($"Task with Id '{taskId}' not found.");

            var user = await _IdentityDbContext.Users.FindAsync(userId.ToString());

            if (user == null)
                throw new NotFoundException($"User with Id '{userId}' not found.");

            todo.AssignedUserId = userId;

            _dbContext.todos.Update(todo);
            await _dbContext.SaveChangesAsync();
        }
    }
}
