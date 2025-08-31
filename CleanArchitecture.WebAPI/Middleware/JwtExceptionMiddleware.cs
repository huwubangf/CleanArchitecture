using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.WebAPI.Middleware
{
    public class JwtExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (SecurityTokenExpiredException)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"error\":\"Token has expired\"}");
            }
            catch (SecurityTokenException)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"error\":\"Invalid token\"}");
            }
            catch (Exception)
            {
                throw; 
            }
        }
    }

}
