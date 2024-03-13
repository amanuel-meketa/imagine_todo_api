using AutoMapper;
using Imagine_todo.application.Contracts.Persistence;
using Imagine_todo.application.Dtos;
using Imagine_todo.application.Features.Todos.Request.Commands;
using Imagine_todo.domain;
using MediatR;

namespace Imagine_todo.application.Features.Todos.Handler.Commands
{
    public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, Guid>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public CreateTodoHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = _mapper.Map<Todo>(request.todoCreateDto);
                todo = await _todoRepository.Add(todo);

            return todo.Id;
        }
    }
}
