using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Api
{
    public class ExceptionsMiddleware
    {
        private static Dictionary<Type, Func<HttpStatusCode>> _map = new Dictionary<Type, Func<HttpStatusCode>> {
            { typeof(DbException), () => HttpStatusCode.InternalServerError },
            { typeof(NotFoundException), () => HttpStatusCode.NotFound },
            { typeof(NotSupportedException), () =>  HttpStatusCode.BadRequest },
            { typeof(ArgumentNullException), () =>  HttpStatusCode.BadRequest },
            { typeof(ArgumentOutOfRangeException), () =>  HttpStatusCode.BadRequest },
            { typeof(ArgumentException), () =>  HttpStatusCode.BadRequest },
            { typeof(HttpRequestException), () =>  HttpStatusCode.BadRequest },
            { typeof(AuthenticationException), () =>  HttpStatusCode.Unauthorized },
            { typeof(UnauthorizedAccessException), () =>  HttpStatusCode.Forbidden },
        };
        
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionsMiddleware> _logger;

        public ExceptionsMiddleware(RequestDelegate next,  ILogger<ExceptionsMiddleware> logger)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }      

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)ExtractHttpStatus(exception);
            context.Response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(new
            {
                error = exception.Message
            });
            
            await context.Response.WriteAsync(result);
            
            _logger.LogError(exception, exception.Message);
        }

        private static HttpStatusCode ExtractHttpStatus<T>(T exception) where T : Exception
        {
            var any = _map.TryGetValue(exception.GetType(), out var action);
            if (!any)
            {
                return HttpStatusCode.InternalServerError;
            }

            return action();
        }
    }
}
