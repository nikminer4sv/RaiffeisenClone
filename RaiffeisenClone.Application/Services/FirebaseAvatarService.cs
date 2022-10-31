using System.Text;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RaiffeisenClone.Application.Interfaces;

namespace RaiffeisenClone.Application.Services;

public class FirebaseAvatarService : IAvatarService
{
    public async Task<Guid> UploadAsync(Guid userId, IFormFile file)
    {
        var storage = new FirebaseStorage("raiffeisen-5c832.appspot.com", new FirebaseStorageOptions
        {
            ThrowOnCancel = true,
        });
        var avatarId = Guid.NewGuid();
        using (var fs = new MemoryStream())
        {
            await file.CopyToAsync(fs);
            fs.Seek(0, SeekOrigin.Begin);
            await storage
                .Child("Images")
                .Child(avatarId.ToString())
                .PutAsync(fs);
        }
        return avatarId;
    }
}