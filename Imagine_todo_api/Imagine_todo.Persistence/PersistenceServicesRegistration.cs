using Imagine_todo.application.Contracts.Persistence;
using Imagine_todo.Persistence.Repositorys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data.SqlClient;

namespace Imagine_todo.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                AddDbContext(options, configuration);
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ITodoRepository, TodoRepository>();
            return services;
        }

        private static void AddDbContext(DbContextOptionsBuilder options, IConfiguration configuration)
        {
            const string DBMS_POSTGRES = "Postgres";

            var databaseType = configuration["AppSettings:Database:Driver"];
            var connectionString = configuration["AppSettings:Database:ConnectionString"];
            var user = configuration["AppSettings:Database:UserName"];
            var password = configuration["AppSettings:Database:Password"];

            if (DBMS_POSTGRES.Equals(databaseType, StringComparison.OrdinalIgnoreCase))
            {
                var builder = new NpgsqlConnectionStringBuilder(connectionString);

                if (!string.IsNullOrWhiteSpace(user))
                    builder.Username = user;

                if (!string.IsNullOrWhiteSpace(password))
                    builder.Password = password;

                options.UseNpgsql(builder.ConnectionString,
                    x => x.MigrationsHistoryTable(ApplicationDbContext.SchemaTableName));
            }
            else
                throw new ArgumentException($"Unsupported database type: {databaseType}");
        }
    }
}
