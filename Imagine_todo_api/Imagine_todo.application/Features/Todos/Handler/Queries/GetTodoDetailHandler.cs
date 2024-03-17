using AutoMapper;
using Imagine_todo.application.Contracts.Persistence;
using Imagine_todo.application.Dtos;
using Imagine_todo.application.Features.Todos.Request.Queries;
using MediatR;

namespace Imagine_todo.application.Features.Todos.Handler.Queries
{
    public class GetTodoDetailHandler : IRequestHandler<GetTodoDetailRequest, TodoDto>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public GetTodoDetailHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<TodoDto> Handle(GetTodoDetailRequest request, CancellationToken cancellationToken)
        {
            var respons = await _todoRepository.Get(request.Id);

            return _mapper.Map<TodoDto>(respons);
        }
    }
}
