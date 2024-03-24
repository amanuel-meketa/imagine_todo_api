using AutoMapper;
using Imagine_todo.application.Dtos;
using Imagine_todo.application.Dtos.Identity;
using Imagine_todo.domain;

namespace Imagine_todo.application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Todo, TodoDto>().ReverseMap();
            CreateMap<Todo, TodoCreateDto>().ReverseMap();
            CreateMap<Todo, TodoListDto>().ReverseMap();
            CreateMap<Todo, TodoUpdateDto>().ReverseMap();

            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<ApplicationUser, UpdateUserDto>().ReverseMap();
        }
    }
}
