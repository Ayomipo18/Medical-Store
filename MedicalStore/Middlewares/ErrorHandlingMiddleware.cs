using Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Shared.Helpers;
using System.Net;

namespace MedicalStore.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, ILoggerManager logger)
        {
            object errors = null;
            var message = string.Empty;
            switch (ex)
            {
                case RestException re:
                    logger.LogError(ex.Message);
                    message = re.ErrorMessage;
                    errors = re.Errors;
                    context.Response.StatusCode = (int)re.Code;
                    break;
                case ArgumentNullException re:
                    logger.LogError(ex.Message);
                    message = re.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case Exception e:
                    logger.LogError(ex.Message);
                    message = e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;

            }
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            var response = new ErrorResponse<object>
            {
                Message = message,
                Error = errors
            };

            context.Response.ContentType = "application/json";

            var result = JsonConvert.SerializeObject(response, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
            await context.Response.WriteAsync(result);
        }
    }

    public static class ErrorHandlingMiddlewareExtension
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
