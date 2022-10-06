using System.ComponentModel.DataAnnotations;

namespace RaiffeisenClone.Domain;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
}