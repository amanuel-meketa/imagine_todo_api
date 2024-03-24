using Imagine_todo.Persistence;
using Imagine_todo.application;
using Imagine_todo.Identity;
using Microsoft.OpenApi.Models;
using System.Reflection;
using MediatR;
using Imagine_todo_api.Middleware;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

AddServices(builder);

var app = builder.Build();

ConfigureMiddleware(app);

app.Run();

void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
    builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
    builder.Services.ConfigureApplicationServices();
    builder.Services.ConfigurePersistenceServices(builder.Configuration);
    builder.Services.ConfigureIdentityServices(builder.Configuration);
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<ExceptionHandlingMiddleware>();
    AddSwaggerDoc(builder.Services);
    AddRateLimiter(builder.Services);
}

void ConfigureMiddleware(WebApplication app)
{
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseAuthentication();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        });
    }
    app.UseAuthorization();
    app.MapControllers();
    app.UseRateLimiter();
    ApplyDatabaseMigrations(app.Services);
}
void ApplyDatabaseMigrations(IServiceProvider serviceProvider)
{
    using (var scope = serviceProvider.CreateScope())
    {
        // Apply migrations for application DbContext (tasks)
        var todoContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        todoContext.Database.Migrate();
        todoContext.SaveChanges();

        // Apply migrations for identity DbContext
        var identityContext = scope.ServiceProvider.GetRequiredService<TodoIdentityDbContext>();
        identityContext.Database.Migrate();
        identityContext.SaveChanges();
    }
}
void AddRateLimiter(IServiceCollection services)
{
    services.AddRateLimiter(option =>
    {
        option.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        option.AddPolicy("FixedPolicy", httpContent =>RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContent.User.Identity?.Name?.ToString(),
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10,
                Window = TimeSpan.FromSeconds(30)
            }));
    });
}
void AddSwaggerDoc(IServiceCollection services)
{
    services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });

        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Imagine Task Api",
        });
    });
}
