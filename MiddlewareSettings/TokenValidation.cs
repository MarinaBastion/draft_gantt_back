using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
 
namespace CustomMiddleware.Test.CustomMiddleware
{
    public class JwtTimeValidator
    {
        private readonly RequestDelegate _next;
 
        public JwtTimeValidator(RequestDelegate next)
        {
            _next = next;
        }
 
        public async Task InvokeAsync(HttpContext context)
        {
            var authorizationHeader = context.Request.Headers["Authorization"].ToString();
            if(!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.Length >11 )
            {
                // Validation of the Token expiration
                var jwt = authorizationHeader.Replace("Bearer ", string.Empty);
                var token = jwt;
                var handler = new JwtSecurityTokenHandler();
                var tokenData = handler.ReadJwtToken(token);
                        
                if (DateTime.Compare(DateTime.UtcNow, tokenData.ValidTo) > 0)
                {
                    context.Response.Clear();
                    context.Response.StatusCode = 440;
                    await context.Response.WriteAsync("Login Time-out");
                }
                else
                {
                    await _next(context);
                }
                }
            else
            {
                await _next(context);
                // context.Response.Clear();
                // context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                // await context.Response.WriteAsync("Unauthorized");
            }
        }
    }
}