using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError; //Normalde statuscode 500 olarak international server olarak atanır.

            string message = "Internal Server Error"; 
            IEnumerable<ValidationFailure> errors; 
            if (e.GetType() == typeof(ValidationException)) // Ama international server hatası değil de validasyon hatası olduğunu anladığında status codu 400 bad request olarak atar.
            {
                // e : doğrulama hatası 
                message = e.Message;
                errors = ((ValidationException)e).Errors;
                httpContext.Response.StatusCode = 400; // Bad request.
                return httpContext.Response.WriteAsync(new ValidationErrorDetalis
                {
                    StatusCode = 400,
                    Message = message,
                    Errors = errors
                }.ToString()); 

            }
            // Bu sistemsel hatada çalışır;
            return httpContext.Response.WriteAsync(new ErrorDetails 
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
