using Imagine_todo.application.Exceptions;
using process.Application.Model;
using System.Net;

namespace Imagine_todo_api.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await HandleExceptionAsync(context, ex);
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest; 
                await HandleExceptionAsync(context, ex);
            }
            catch (ConflictException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                await HandleExceptionAsync(context, ex);
            }
            catch (BadRequestException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string errorMessage = "An unexpected error occurred.";
            if (ex is System.ComponentModel.DataAnnotations.ValidationException validationException)
            {
                errorMessage = validationException.Message; // Include validation error message
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest; // Set status code for validation errors
            }

            // You may want to customize this ErrorDetails object to suit your application's needs
            var errorDetails = new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = errorMessage,
                StackTrace = ex.StackTrace,
                Source = ex.Source
            };

            return context.Response.WriteAsync(errorDetails.ToString());
        }
    }
}
