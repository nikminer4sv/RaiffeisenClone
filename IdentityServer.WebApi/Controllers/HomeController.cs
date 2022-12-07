using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.WebApi.Controllers;

public class HomeController : Controller
{
    [Authorize]
    [HttpGet]
    public IActionResult info()
    {
        return Ok("From authorized method");
    }
}