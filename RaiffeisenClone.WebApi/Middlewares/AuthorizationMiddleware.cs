using RaiffeisenClone.Application.Interfaces;

namespace RaiffeisenClone.WebApi.Middlewares;

public class AuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthorizationMiddleware(RequestDelegate next) =>
        (_next) = (next);

    public async Task Invoke(HttpContext context, IUserService userService, IJwtService jwtService)
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