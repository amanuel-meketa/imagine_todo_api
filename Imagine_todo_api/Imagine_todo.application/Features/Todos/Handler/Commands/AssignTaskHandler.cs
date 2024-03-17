using AutoMapper;
using Imagine_todo.application.Contracts.Persistence;
using Imagine_todo.application.Features.Todos.Request.Commands;
using MediatR;

namespace Imagine_todo.application.Features.Todos.Handler.Commands
{
    public class AssignTaskHandler : IRequestHandler<AssignTaskCommand, Unit>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public AssignTaskHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AssignTaskCommand request, CancellationToken cancellationToken)
        {
            await _todoRepository.AssignTask(request.TodoId, request.UserId);

            return Unit.Value;
        }
    }
}
