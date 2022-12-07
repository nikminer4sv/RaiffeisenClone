using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace IdentityServer.WebApi.Models;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public Guid? AvatarId { get; set; }
}