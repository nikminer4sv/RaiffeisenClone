using IdentityModel.Client;
using IdentityServer.WebApi.Models;
using IdentityServer.WebApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.WebApi.Controllers;

public class AuthController : Controller
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly HttpClient _httpClient;

    public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, HttpClient httpClient)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _httpClient = httpClient;
    }

    public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
    {
        var identityServerResponse = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
        {
            Address = "http://localhost:5171/connect/token",
            GrantType = "password",

            ClientId = "raiffeisen-api",
            ClientSecret = "a75a559d-1dab-4c65-9bc0-f8e590cb388d",
            Scope = "RaiffeisenApi",

            UserName = loginViewModel.Username,
            Password = loginViewModel.Password
        });

        if (!identityServerResponse.IsError)
            return Ok(new AuthenticateResponseViewModel() {AccessToken = identityServerResponse.AccessToken});
        
        return BadRequest("Invalid username or password");
    }

    public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(registerViewModel.FirstName + " " + registerViewModel.LastName + " " + registerViewModel.Email);
        var user = new AppUser
        {
            FirstName = registerViewModel.FirstName,
            LastName = registerViewModel.LastName,
            UserName = registerViewModel.Username,
            Email = registerViewModel.Email,
        };
        var result = await _userManager.CreateAsync(user, registerViewModel.Password);
        if (result.Succeeded)
            await _signInManager.SignInAsync(user, false);
        return Ok();
    }
}