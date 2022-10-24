using Microsoft.AspNetCore.Http;
using RaiffeisenClone.Application.Interfaces;
using RaiffeisenClone.Domain;
using RaiffeisenClone.Persistence.Interfaces;

namespace RaiffeisenClone.Application.Services;

public class AvatarService : IAvatarService
{
    private readonly IUnitOfWork _unitOfWork;

    public AvatarService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
    
    public async Task<Guid> UploadAsync(Guid userId, IFormFile file)
    {
        byte[] avatarImage;
        
        using (var ms = new MemoryStream())
        {
            await file.CopyToAsync(ms);
            avatarImage = ms.ToArray();
        }

        var avatar = new Avatar
        {
            Image = avatarImage
        };
        
        var avatarId = await _unitOfWork.Avatars.AddAsync(avatar);
        
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        user.AvatarId = avatarId;
        await _unitOfWork.Users.UpdateAsync(user);
        
        await _unitOfWork.SaveAsync();

        return avatarId;
    }
}