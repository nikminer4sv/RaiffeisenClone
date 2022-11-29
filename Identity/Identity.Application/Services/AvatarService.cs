using Identity.Application.Interfaces;
using Identity.Persistence.Interfaces;
using Microsoft.AspNetCore.Http;
using RaiffeisenClone.Domain;

namespace Identity.Application.Services;

public class AvatarService : IAvatarService
{
    private readonly IUserRepository _userRepository;
    private readonly IAvatarRepository _avatarRepository;

    public AvatarService(IUserRepository userRepository, IAvatarRepository avatarRepository) => 
        (_userRepository, _avatarRepository) = (userRepository, avatarRepository);
    
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
        
        var avatarId = await _avatarRepository.AddAsync(avatar);
        
        var user = await _userRepository.GetByIdAsync(userId);
        user.AvatarId = avatarId;
        await _userRepository.UpdateAsync(user);
        
        await _userRepository.SaveAsync();

        return avatarId;
    }
}