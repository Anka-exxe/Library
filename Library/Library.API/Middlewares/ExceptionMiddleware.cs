using System.Net;
using Library.Application.Exceptions;
using Newtonsoft.Json;

namespace Library.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;

            switch (exception)
            {
                case BadRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;
                case TokenExpiredException:
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                case InternalServerErrorException:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
                default:
                    _logger.LogError(exception, "Unhandled exception occurred at {Path}", context.Request.Path);
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message,
                Detailed = statusCode == HttpStatusCode.InternalServerError
                           ? "An unexpected error occurred."
                           : exception.Message
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
