using Imagine_todo.application.Dtos.Common;

namespace Imagine_todo.application.Dtos.Identity
{
    public class UserDto : BaseEntityDto
    {
       public string? FirstName { get; set; }
       public string? LastName { get; set; }
       public string? UserName { get; set; }
       public string? Email { get; set; }
    }
}
