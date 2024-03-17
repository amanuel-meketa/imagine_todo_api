using AutoMapper;
using Imagine_todo.application.Contracts.Persistence;
using Imagine_todo.application.Dtos;
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
            await ValidateTodoUpdateDtoAsync(request.todoDto);

            var todo = await _todoRepository.Get(request.todoDto.Id);

            _mapper.Map(request.todoDto, todo);
            await _todoRepository.Update(todo);

            return Unit.Value;
        }

        private async Task ValidateTodoUpdateDtoAsync(TodoDto todoDto)
        {
            var validator = new UpdateTodoDtoValidator();
            var validatorResult = await validator.ValidateAsync(todoDto);

            if (!validatorResult.IsValid)
            {
                var errors = validatorResult.Errors.Select(e => e.ErrorMessage);
                var errorMessage = string.Join(Environment.NewLine, errors);
                throw new System.ComponentModel.DataAnnotations.ValidationException(errorMessage);
            }
        }
    }
}
