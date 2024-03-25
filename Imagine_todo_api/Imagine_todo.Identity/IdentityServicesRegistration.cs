using Imagine_todo.application.Contracts.Identity;
using Imagine_todo.application.Model.Identity;
using Imagine_todo.domain;
using Imagine_todo.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Npgsql;

namespace Imagine_todo.Identity
{
    public static class IdentityServicesRegistration
    {
        public const string SchemaTableName = "__EFMigrationsHistory";
        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TodoIdentityDbContext>(options =>
            {
                AddDbContext(options, configuration);
            });
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<TodoIdentityDbContext>().AddDefaultTokenProviders();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                    };
                });

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
                    x => x.MigrationsHistoryTable(TodoIdentityDbContext.SchemaTableNameAppDB));
            }
            else
                throw new ArgumentException($"Unsupported database type: {databaseType}");
        }

    }
}
