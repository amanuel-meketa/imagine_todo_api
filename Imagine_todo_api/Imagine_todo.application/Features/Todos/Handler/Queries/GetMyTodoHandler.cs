using AutoMapper;
using Imagine_todo.application.Contracts.Persistence;
using Imagine_todo.application.Dtos;
using Imagine_todo.application.Features.Todos.Request.Queries;
using MediatR;

namespace Imagine_todo.application.Features.Todos.Handler.Queries
{
    public class GetMyTodoHandler : IRequestHandler<GetMyTodoReuest, List<TodoDto>>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public GetMyTodoHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public Task<List<TodoDto>> Handle(GetMyTodoReuest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
