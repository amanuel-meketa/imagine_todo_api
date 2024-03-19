using Imagine_todo.application.Contracts.Persistence;
using Imagine_todo.application.Features.Todos.Request.Commands;
using MediatR;

namespace Imagine_todo.application.Features.Todos.Handler.Commands
{
    public class AssignTaskHandler : IRequestHandler<AssignTaskCommand, Unit>
    {
        private readonly ITodoRepository _todoRepository;

        public AssignTaskHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<Unit> Handle(AssignTaskCommand request, CancellationToken cancellationToken)
        {
            await _todoRepository.AssignTask(request.TodoId, request.UserId);

            return Unit.Value;
        }
    }
}
