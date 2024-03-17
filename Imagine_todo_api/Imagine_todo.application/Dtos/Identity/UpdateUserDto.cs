using Imagine_todo.application.Dtos.Common;

namespace Imagine_todo.application.Dtos.Identity
{
    public class UpdateUserDto : BaseEntityDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
    }
}
