using Microsoft.AspNetCore.Http;

namespace Identity.Application.Interfaces;

public interface IAvatarService
{
    Task<Guid> UploadAsync(Guid userId, IFormFile file);
}