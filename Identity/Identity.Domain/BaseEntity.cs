using System.ComponentModel.DataAnnotations;

namespace Identity.Domain;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
}