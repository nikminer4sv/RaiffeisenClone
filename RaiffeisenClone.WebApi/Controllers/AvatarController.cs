using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RaiffeisenClone.WebApi.Controllers;

[Authorize]
[ApiController]
public class AvatarController : BaseController
{
    private readonly HttpClient _httpClient;

    public AvatarController(HttpClient httpClient) => _httpClient = httpClient;

    [HttpPost]
    public async Task<IActionResult> Upload([FromForm]IFormFile avatar)
    {
        var content = new StringContent(JsonSerializer.Serialize(avatar), Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);
        var response = await _httpClient.PostAsync("http://localhost:5073/api/avatar", content);
        response.EnsureSuccessStatusCode();
        return Ok(await response.Content.ReadAsStringAsync());
    }
    
}