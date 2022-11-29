using Identity.Domain;

namespace RaiffeisenClone.Domain;

public class Avatar : BaseEntity
{
    public byte[] Image { get; set; }
}