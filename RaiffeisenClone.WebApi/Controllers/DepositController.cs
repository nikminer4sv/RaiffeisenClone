using Microsoft.AspNetCore.Mvc;
using RaiffeisenClone.Application.Services;
using RaiffeisenClone.Application.ViewModels;
using RaiffeisenClone.Domain;
using RaiffeisenClone.Persistence.Repositories;

namespace RaiffeisenClone.WebApi.Controllers;

[ApiController]
[Route("/api/[controller]/[action]")]
public class DepositController : ControllerBase
{
    private readonly DepositService _depositService;

    public DepositController(DepositService depositService) => (_depositService) = (depositService);

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _depositService.GetAllAsync());
    }

    [HttpGet]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _depositService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Add(DepositViewModel depositViewModel)
    {
        return Created("", await _depositService.AddAsync(depositViewModel));
    }

    [HttpPut]
    public async Task<IActionResult> Update(Deposit deposit)
    {
        await _depositService.UpdateAsync(deposit);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _depositService.DeleteAsync(id);
        return NoContent();
    }
    
}