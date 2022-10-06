using Microsoft.AspNetCore.Mvc;
using RaiffeisenClone.Application.Interfaces;
using RaiffeisenClone.Application.Services;
using RaiffeisenClone.Application.ViewModels;
using RaiffeisenClone.Application.ViewModels.IdViewModel;
using RaiffeisenClone.Domain;
using RaiffeisenClone.WebApi.Attributes;

namespace RaiffeisenClone.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class DepositController : ControllerBase
{
    private readonly IDepositService _depositService;

    private Guid UserId
    {
        get
        {
            User user = (User) HttpContext.Items["User"];
            return user.Id;
        }
    }

    public DepositController(IDepositService depositService) => 
        (_depositService) = (depositService);

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _depositService.GetAllAsync(UserId));
    }

    [HttpGet("/api/[controller]/{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        return Ok(await _depositService.GetByIdAsync(id, UserId));
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] DepositViewModel depositViewModel)
    {
        return Created("", await _depositService.AddAsync(depositViewModel, UserId));
    }

    [HttpPut("/api/[controller]/")]
    public async Task<IActionResult> Update([FromBody] DepositUpdateViewModel depositUpdateViewModel)
    {
        return Ok(await _depositService.UpdateAsync(depositUpdateViewModel, UserId));
    }

    [HttpDelete("/api/[controller]/{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _depositService.DeleteAsync(id, UserId);
        return Ok(id);
    }

}