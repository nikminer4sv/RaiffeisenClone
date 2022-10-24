using RaiffeisenClone.Domain;
using RaiffeisenClone.Persistence.Interfaces;

namespace RaiffeisenClone.Persistence.Repositories;

public class AvatarRepository : GenericRepository<Avatar>, IAvatarRepository 
{
    public AvatarRepository(ApplicationDbContext context) : base(context)
    {
    }
}