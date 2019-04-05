using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Token.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AuthenticationException)
            {
                var message = new
                {
                    message = "Falha de autenticação",
                    status = StatusCodes.Status401Unauthorized
                };

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsync(JsonConvert.SerializeObject(message));
            }
            catch (Exception ex)
            {
                var message = new
                {
                    message = "Erro desconhecido",
                    status = StatusCodes.Status500InternalServerError
                };

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsync(JsonConvert.SerializeObject(message));
            }
        }
    }
}
