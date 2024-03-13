using AutoMapper;
using Imagine_todo.application.Contracts.Persistence;
using Imagine_todo.application.Features.Todos.Request.Commands;
using MediatR;

namespace Imagine_todo.application.Features.Todos.Handler.Commands
{
    public class DeleteTodoHandler : IRequestHandler<DeleteTodoCommand>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public DeleteTodoHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var response = await _todoRepository.Get(request.Id);

            await _todoRepository.Delete(response);
            return Unit.Value;
        }
    }
}
