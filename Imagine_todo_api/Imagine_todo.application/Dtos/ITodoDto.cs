
namespace Imagine_todo.application.Dtos
{
    public interface ITodoDto
    {
        public string Title { get; set; } 
        public string Description { get; set; } 
        public DateTime DueDate { get; set; }
    }
}
