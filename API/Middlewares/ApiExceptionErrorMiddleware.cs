using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Middlewares
{
    public class ApiExceptionErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionErrorMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ApiExceptionErrorMiddleware(RequestDelegate next, ILogger<ApiExceptionErrorMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment() ?
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace) :
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

                var json = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                await context.Response.WriteAsync(json);
            }
        }
    }
}
