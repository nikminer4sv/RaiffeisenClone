using Identity.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebApi.Controllers;

[Authorize]
[ApiController]
public class AvatarController : BaseController
{
    private readonly IAvatarService _avatarService;

    public AvatarController(IAvatarService avatarService) => _avatarService = avatarService;

    [HttpPost]
    public async Task<IActionResult> Upload([FromForm]IFormFile avatar)
    {
        if (avatar.Length > 0)
        {
            var id = await _avatarService.UploadAsync(UserId, avatar);
            return Ok(id);
        }
        return BadRequest();
    }
    
}