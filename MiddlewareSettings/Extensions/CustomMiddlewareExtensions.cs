using Microsoft.AspNetCore.Builder;
 
namespace CustomMiddleware.Test.CustomMiddleware
{
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtTimeValidator(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtTimeValidator>();
        }
    }
}