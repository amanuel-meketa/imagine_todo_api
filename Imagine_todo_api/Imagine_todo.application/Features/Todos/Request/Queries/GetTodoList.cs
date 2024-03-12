using Imagine_todo.application.Dtos;
using MediatR;

namespace Imagine_todo.application.Features.Todos.Request.Queries
{
    public class GetTodoListRequest : IRequest<List<TodoListDto>>
    {
    }
}
