using Microsoft.AspNetCore.Mvc;
using RaiffeisenClone.Domain;
using RaiffeisenClone.Persistence.Repositories;

namespace RaiffeisenClone.WebApi.Controllers;

[ApiController]
[Route("[action]")]
public class TestController : ControllerBase
{
    private readonly UserRepository _userRepository;

    public TestController(UserRepository userRepository) => (_userRepository) = (userRepository);
    
    public async Task<IActionResult> test()
    {
        User user = new User()
        {
            FirstName = "Nikita",
            LastName = "Korotki",
            DateOfBirth = DateTime.Now
        };
        await _userRepository.AddAsync(user);
        await _userRepository.SaveAsync();
        return Content("Hello");
    }

    public async Task test2()
    {
        var user = await _userRepository.GetByIdAsync(Guid.Parse("662D0F1D-66FC-467A-BBD8-C12318925798"));
        user!.FirstName = "Ne nikita x2";
        await _userRepository.DeleteAsync(user.Id);
        await _userRepository.SaveAsync();
    }
}