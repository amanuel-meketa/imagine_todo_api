using Microsoft.EntityFrameworkCore;
using Imagine_todo.Persistence;
using Microsoft.OpenApi.Models;
using Microsoft.Data.SqlClient;
using Npgsql;
using Imagine_todo.application.Contracts.Persistence;
using Imagine_todo.Persistence.Repositorys;
using System.Reflection;

namespace YourNamespace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo API", Version = "v1" });
            });

            const string DBMS_SQL_SERVER = "SQLServer";
            const string DBMS_POSTGRES = "Postgres";

            var databaseType = Configuration["AppSettings:Database:Driver"];
            var connectionString = Configuration["AppSettings:Database:ConnectionString"];
            var user = Configuration["AppSettings:Database:UserName"];
            var password = Configuration["AppSettings:Database:Password"];

            if (DBMS_SQL_SERVER.Equals(databaseType, StringComparison.OrdinalIgnoreCase))
            {
                var builder = new SqlConnectionStringBuilder(connectionString);

                if (!string.IsNullOrWhiteSpace(password))
                    builder.Password = password;

                if (!string.IsNullOrWhiteSpace(user))
                    builder.UserID = user;

                services.AddDbContext<ApplicationDbContext>(
                    options => options.UseSqlServer(builder.ConnectionString)
                );
            }
            else if (DBMS_POSTGRES.Equals(databaseType, StringComparison.OrdinalIgnoreCase))
            {
                var builder = new NpgsqlConnectionStringBuilder(connectionString);

                if (!string.IsNullOrWhiteSpace(user))
                    builder.Username = user;

                if (!string.IsNullOrWhiteSpace(password))
                    builder.Password = password;

                services.AddDbContext<ApplicationDbContext>(
                    options => options.UseNpgsql(builder.ConnectionString)
                );
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Imagine todo API V1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
