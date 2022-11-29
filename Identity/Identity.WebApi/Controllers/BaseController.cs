using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebApi.Controllers;

[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    protected Guid UserId => User.Identity!.IsAuthenticated
        ? Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        : Guid.Empty;
}