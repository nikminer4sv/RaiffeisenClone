using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace RaiffeisenClone.WebApi.Controllers;

[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    protected Guid UserId => Guid.Parse(User.FindFirst("id").Value);
}