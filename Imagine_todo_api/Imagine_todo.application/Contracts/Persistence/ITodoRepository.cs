using Imagine_todo.domain;

namespace Imagine_todo.application.Contracts.Persistence
{
    public interface ITodoRepository : IGenericRepository<Todo>
    {
        Task AssignTask(Guid taskId, Guid userId);
        Task<List<Todo>> GetMyTasks(Guid userId);
        Task ComplateTask(Guid taskID, string Status);
    }
}
