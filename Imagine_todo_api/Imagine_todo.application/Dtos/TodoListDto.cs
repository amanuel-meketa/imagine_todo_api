namespace Imagine_todo.application.Dtos
{
    public class TodoListDto
    {
        public string Title { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
