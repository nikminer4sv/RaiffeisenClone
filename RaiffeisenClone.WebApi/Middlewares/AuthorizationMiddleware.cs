using RaiffeisenClone.Application.Services;

namespace RaiffeisenClone.WebApi.Middlewares;

public class AuthorizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public AuthorizationMiddleware(RequestDelegate next, IConfiguration configuration) =>
        (_next, _configuration) = (next, configuration);

    public async Task Invoke(HttpContext context, UserService userService, JwtService jwtService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        Guid? userId = jwtService.ValidateJwtToken(token);
        if (userId != null)
        {
            // attach user to context on successful jwt validation
            context.Items["User"] = await userService.GetByIdAsync(userId.Value);
        }
        await _next(context);
    }
    
}