using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RaiffeisenClone.Application.ViewModels;

namespace RaiffeisenClone.WebApi.Controllers;

[ApiController]
public class AuthController : BaseController
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AuthController> _logger;

    public AuthController(HttpClient httpClient, ILogger<AuthController> logger) => 
        (_httpClient, _logger) = (httpClient, logger);

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] LoginViewModel model)
    {
        _logger.LogCritical(UserId.ToString());
        var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);
        var response = await _httpClient.PostAsync("http://localhost:5073/api/auth/login", content);
        response.EnsureSuccessStatusCode();
        return Ok(await response.Content.ReadAsStringAsync());
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
    {
        var content = new StringContent(JsonSerializer.Serialize(registerViewModel), Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);
        var response = await _httpClient.PostAsync("http://localhost:5073/api/auth/register", content);
        response.EnsureSuccessStatusCode();
        return Ok(await response.Content.ReadAsStringAsync());
    }
    
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var content = new StringContent(JsonSerializer.Serialize(refreshToken), Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);
        var response = await _httpClient.PostAsync("http://localhost:5073/api/auth/refresh-token", content);
        response.EnsureSuccessStatusCode();
        return Ok(await response.Content.ReadAsStringAsync());
    }
    
    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeToken([FromBody] string? refreshToken)
    {
        var token = (refreshToken is null) ? Request.Cookies["refreshToken"] : refreshToken;

        if (string.IsNullOrEmpty(token))
            return BadRequest(new { message = "Token is required" });

        var content = new StringContent(JsonSerializer.Serialize(refreshToken), Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);
        var response = await _httpClient.PostAsync("http://localhost:5073/api/auth/revoke-token", content);
        response.EnsureSuccessStatusCode();
        return Ok(await response.Content.ReadAsStringAsync());
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