using Imagine_todo.application.Contracts.Persistence;
using Imagine_todo.domain;

namespace Imagine_todo.Persistence.Repositorys
{
    public class TodoRepository : GenericRepository<Todo>, ITodoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TodoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
