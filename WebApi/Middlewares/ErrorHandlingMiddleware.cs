using Application.Exceptions;
using Application.Models;
using System.Net;
using System.Text.Json;

namespace WebApi.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                ErrorModel error = new();

                switch(ex)
                {
                    case CustomValidationException validationEx:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        error.Message = ex.Message;
                        error.ErrorsMessages = validationEx.ErrorMessages;
                        break;

                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        error.Message = ex.Message;
                        break;
                }

                var result = JsonSerializer.Serialize(error);
                await response.WriteAsync(result);
            }
        }
    }
}
