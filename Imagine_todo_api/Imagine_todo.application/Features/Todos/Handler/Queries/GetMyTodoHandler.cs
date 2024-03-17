using AutoMapper;
using Imagine_todo.application.Contracts.Persistence;
using Imagine_todo.application.Dtos;
using Imagine_todo.application.Features.Todos.Request.Queries;
using MediatR;

namespace Imagine_todo.application.Features.Todos.Handler.Queries
{
    public class GetMyTodoHandler : IRequestHandler<GetMyTodoRequest, List<TodoDto>>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public GetMyTodoHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<List<TodoDto>> Handle(GetMyTodoRequest request, CancellationToken cancellationToken)
        {
            var response = await _todoRepository.GetMyTasks(request.Id);

            return _mapper.Map<List<TodoDto>>(response);
        }
    }
}
