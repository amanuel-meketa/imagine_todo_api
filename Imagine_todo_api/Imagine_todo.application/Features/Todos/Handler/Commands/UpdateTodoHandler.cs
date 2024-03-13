using AutoMapper;
using Imagine_todo.application.Contracts.Persistence;
using Imagine_todo.application.Dtos.Validator;
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
            var validator = new UpdateTodoDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.todoDto);

            if (!validatorResult.IsValid)
                throw new Exception("validation error");

            var respons = await _todoRepository.Get(request.todoDto.Id);
            _mapper.Map(request.todoDto, respons);
            await _todoRepository.Update(respons);

            return Unit.Value;
        }
    }
}
