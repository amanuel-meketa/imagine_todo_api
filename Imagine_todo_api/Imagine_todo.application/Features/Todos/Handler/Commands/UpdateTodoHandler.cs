using AutoMapper;
using Imagine_todo.application.Contracts.Persistence;
using Imagine_todo.application.Features.Todos.Request.Commands;
using MediatR;

namespace Imagine_todo.application.Features.Todos.Handler.Commands
{
    public class UpdateTodoHandler : IRequestHandler<UpdateTodoCommand, Unit>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public UpdateTodoHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            var respons = await _todoRepository.Get(request.todoDto.Id);
            _mapper.Map(request.todoDto, respons);

            return Unit.Value;
        }
    }
}
