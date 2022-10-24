using Firebase.Database;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using RaiffeisenClone.Application.Interfaces;
using RaiffeisenClone.Domain;

namespace RaiffeisenClone.Application.Services;

public class FirebaseAvatarService : IAvatarService
{
    public async Task<Guid> UploadAsync(Guid userId, IFormFile file)
    {
        Guid avatarId = Guid.NewGuid();
        byte[] avatarImage;
        
        using (var ms = new MemoryStream())
        {
            await file.CopyToAsync(ms);
            avatarImage = ms.ToArray();
        }

        var avatar = new Avatar
        {
            Id = avatarId,
            Image = avatarImage
        };
        
        var firebaseClient = new FirebaseClient("https://raiffeisen-5c832-default-rtdb.europe-west1.firebasedatabase.app/");
        var jsonAvatar = JsonSerializer.Serialize(avatar);
        await firebaseClient
            .Child("Avatars/")
            .PostAsync(jsonAvatar);
        return avatarId;
    }
}