using Microsoft.AspNetCore.Mvc;
using RaiffeisenClone.Application.Interfaces;
using RaiffeisenClone.Application.Services;
using RaiffeisenClone.Application.ViewModels;

namespace RaiffeisenClone.WebApi.Controllers;

[ApiController]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) => 
        (_authService) = (authService);

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel model)
    {
        var response = await _authService.Authenticate(model, ipAddress());
        //setTokenCookie(response.RefreshToken);
        return Ok(response);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
    {
        await _authService.Register(registerViewModel);
        return Ok();
    }
    
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var response = await _authService.RefreshToken(refreshToken, ipAddress());
        setTokenCookie(response.RefreshToken);
        return Ok(response);
    }
    
    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeToken([FromBody] string? refreshToken)
    {
        var token = (refreshToken is null) ? Request.Cookies["refreshToken"] : refreshToken;

        if (string.IsNullOrEmpty(token))
            return BadRequest(new { message = "Token is required" });

        await _authService.RevokeToken(token, ipAddress());
        return Ok(new { message = "Token revoked" });
    }

    private void setTokenCookie(string token)
    {
        // append cookie with refresh token to the http response
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7)
        };
        Response.Cookies.Append("refreshToken", token, cookieOptions);
    }
    
    private string ipAddress()
    {
        // get source ip address for the current request
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
        return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }
}