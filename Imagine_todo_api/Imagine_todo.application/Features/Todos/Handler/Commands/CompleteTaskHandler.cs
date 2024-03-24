using Imagine_todo.application.Contracts.Persistence;
using Imagine_todo.application.Features.Todos.Request.Commands;
using MediatR;

namespace Imagine_todo.application.Features.Todos.Handler.Commands
{
    public class CompleteTaskHandler : IRequestHandler<CompleteTaskCommand, Unit>
    {
        private readonly ITodoRepository _todoRepository;

        public CompleteTaskHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<Unit> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
        {
            await _todoRepository.ComplateTask(request.ID, request.Status);

            return Unit.Value;
        }
    }
}
