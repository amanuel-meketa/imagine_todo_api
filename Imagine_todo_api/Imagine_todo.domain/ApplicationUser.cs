using Microsoft.AspNetCore.Identity;

namespace Imagine_todo.domain
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
