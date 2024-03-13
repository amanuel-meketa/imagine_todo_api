using Imagine_todo.application.Dtos;
using MediatR;

namespace Imagine_todo.application.Features.Todos.Request.Queries
{
    public class GetTodoDetailRequest : IRequest<TodoDto>
    {
        public Guid Id { get; set; }
    }
}
