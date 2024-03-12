using AutoMapper;
using Imagine_todo.application.Contracts.Persistence;
using Imagine_todo.application.Dtos;
using Imagine_todo.application.Features.Todos.Request.Queries;
using MediatR;

namespace Imagine_todo.application.Features.Todos.Handler.Queries
{
    public class GetTodoListHandler : IRequestHandler<GetTodoListRequest, List<TodoListDto>>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public GetTodoListHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }
        public async Task<List<TodoListDto>> Handle(GetTodoListRequest request, CancellationToken cancellationToken)
        {
            var respons = await _todoRepository.GetAll();
            return _mapper.Map<List<TodoListDto>>(respons);
        }
    }
}
