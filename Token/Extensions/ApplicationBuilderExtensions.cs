using Microsoft.AspNetCore.Builder;

namespace Token.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseException(this IApplicationBuilder app)
        {
            app.UseMiddleware<Middlewares.ExceptionMiddleware>();
        }

        public static void UseTokenAuthentication(this IApplicationBuilder app)
        {
            app.UseMiddleware<Middlewares.AuthenticationMiddleware>();
        }
    }
}
