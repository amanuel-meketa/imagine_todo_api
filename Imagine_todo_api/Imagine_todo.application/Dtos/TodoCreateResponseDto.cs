using Imagine_todo.domain.Common;

namespace Imagine_todo.application.Dtos
{
    public class TodoCreateResponseDto : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
