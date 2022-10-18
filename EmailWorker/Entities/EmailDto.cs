using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmailWorker.Entities;

public class EmailDto
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [Display(Name = "Email")]
    public string Email { get; set; }
}