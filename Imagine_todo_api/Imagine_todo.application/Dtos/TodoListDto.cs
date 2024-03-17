using Imagine_todo.application.Dtos.Common;

namespace Imagine_todo.application.Dtos
{
    public class TodoListDto : BaseEntityDto
    {
        public string Title { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public Guid AssignedUserId { get; set; }
    }
}
