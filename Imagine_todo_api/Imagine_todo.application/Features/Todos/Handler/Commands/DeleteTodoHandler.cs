using Imagine_todo.application.Contracts.Persistence;
using Imagine_todo.application.Features.Todos.Request.Commands;
using MediatR;

namespace Imagine_todo.application.Features.Todos.Handler.Commands
{
    public class DeleteTodoHandler : IRequestHandler<DeleteTodoCommand>
    {
        private readonly ITodoRepository _todoRepository;

        public DeleteTodoHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }
        public async Task<Unit> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todoDetail = await _todoRepository.Get(request.Id);
            await _todoRepository.Delete(todoDetail);
            return Unit.Value;
        }
    }
}
