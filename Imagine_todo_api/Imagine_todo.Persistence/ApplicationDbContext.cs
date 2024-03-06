using Imagine_todo.domain;
using Microsoft.EntityFrameworkCore;

namespace Imagine_todo.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public const string SchemaTableName = "__EFMigrationsHistory";
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> todos { get; set; }
    }
}
