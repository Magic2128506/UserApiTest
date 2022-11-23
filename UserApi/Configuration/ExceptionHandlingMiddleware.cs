using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace UserApi.Configuration
{
    /// <summary>
    /// Обработчик ошибок API
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Обработчик ошибок API
        /// </summary>
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Действия по обработке запроса ASP.NET
        /// </summary>
        /// <param name="context">Контекст запроса ASP.NET</param>
        /// <returns>Задача на обработку запроса ASP.NET</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
        }

        private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            var errorText = exception.Message;
            var responseCode = HttpStatusCode.BadRequest;
            var details = exception.Errors
                ?.GroupBy(x => x.PropertyName, x => x)
                .ToDictionary(
                    x => x.Key,
                    x => x.Select(e => e.ErrorMessage).ToArray());
            await ReturnExceptionAsync(context, errorText, responseCode, details);
        }

        private async Task ReturnExceptionAsync(HttpContext context,
            string errorText,
            HttpStatusCode responseCode,
            Dictionary<string, string[]> details = null,
            int errorCode = -1)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)responseCode;
            
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    Error = errorText,
                    ErrorCode = errorCode,
                    Success = false,
                    ErrorDetails = details
                },
                new JsonSerializerOptions()));
        }
    }
}
