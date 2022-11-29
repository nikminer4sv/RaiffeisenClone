using Identity.Persistence.Interfaces;
using RaiffeisenClone.Domain;

namespace Identity.Persistence.Repositories;

public class AvatarRepository : GenericRepository<Avatar>, IAvatarRepository 
{
    public AvatarRepository(ApplicationDbContext context) : base(context)
    {
    }
}