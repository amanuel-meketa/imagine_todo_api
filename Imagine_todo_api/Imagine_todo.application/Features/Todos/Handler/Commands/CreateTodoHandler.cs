using AutoMapper;
using Imagine_todo.application.Contracts.Persistence;
using Imagine_todo.application.Dtos;
using Imagine_todo.application.Dtos.Validator;
using Imagine_todo.application.Features.Todos.Request.Commands;
using Imagine_todo.domain;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Imagine_todo.application.Features.Todos.Handler.Commands
{
    public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, TodoCreateResponseDto>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public CreateTodoHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<TodoCreateResponseDto> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            await ValidateTodoCreateDtoAsync(request.todoCreateDto);

            var todo = _mapper.Map<Todo>(request.todoCreateDto);
            var createdTodo = await _todoRepository.Add(todo);

            return _mapper.Map<TodoCreateResponseDto>(createdTodo);
        }

        private async Task ValidateTodoCreateDtoAsync(TodoCreateDto? todoCreateDto)
        {
            var validator = new CreateTodoDtoValidator();
            var validatorResult = await validator.ValidateAsync(todoCreateDto);

            if (!validatorResult.IsValid)
            {
                var errors = validatorResult.Errors.Select(e => e.ErrorMessage);
                var errorMessage = string.Join(Environment.NewLine, errors);
                throw new ValidationException(errorMessage);
            }
        }
    }
}
