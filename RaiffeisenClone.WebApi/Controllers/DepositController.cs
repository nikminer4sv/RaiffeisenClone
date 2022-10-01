using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RaiffeisenClone.Application.Services;
using RaiffeisenClone.Application.ViewModels;
using RaiffeisenClone.Domain;
using RaiffeisenClone.Persistence.Repositories;
using RaiffeisenClone.WebApi.Attributes;

namespace RaiffeisenClone.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("/api/[controller]/[action]")]
public class DepositController : ControllerBase
{
    private readonly DepositService _depositService;

    private Guid UserId
    {
        get
        {
            User user = (User) HttpContext.Items["User"];
            return user.Id;
        }
    }

    public DepositController(DepositService depositService) => 
        (_depositService) = (depositService);

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _depositService.GetAllAsync(UserId));
    }

    [HttpGet]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _depositService.GetByIdAsync(id, UserId));
    }

    [HttpPost]
    public async Task<IActionResult> Add(DepositViewModel depositViewModel)
    {
        return Created("", await _depositService.AddAsync(depositViewModel, UserId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(DepositUpdateViewModel depositUpdateViewModel)
    {
        await _depositService.UpdateAsync(depositUpdateViewModel, UserId);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _depositService.DeleteAsync(id, UserId);
        return NoContent();
    }

}