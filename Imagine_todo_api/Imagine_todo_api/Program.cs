using Imagine_todo.Persistence;
using Imagine_todo.application;
using Imagine_todo.Identity;
using Microsoft.OpenApi.Models;
using System.Reflection;
using MediatR;
using Imagine_todo_api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services
AddServices(builder);

// Build the app
var app = builder.Build();

// Configure middleware
ConfigureMiddleware(app);

// Run the app
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
}

void AddSwaggerDoc(IServiceCollection services)
{
    services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
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
            Title = "Imagine Todo Api",
        });
    });
}
