using Imagine_todo.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System;

namespace YourNamespace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

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
                    options => options.UseSqlServer(
                        builder.ConnectionString,
                        x => x.MigrationsHistoryTable(ApplicationDbContext.SchemaTableName)
                    )
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
                        options => options.UseNpgsql(
                            builder.ConnectionString, x => x.MigrationsHistoryTable(ApplicationDbContext.SchemaTableName)
                        )
                    );
                }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
