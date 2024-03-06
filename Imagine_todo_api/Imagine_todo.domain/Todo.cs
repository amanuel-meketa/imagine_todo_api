using Imagine_todo.domain.Common;

namespace Imagine_todo.domain
{
    public class Todo : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public int Status { get; set; }
    }
}
