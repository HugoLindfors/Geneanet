using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Geneanet.Models;

public class ParentRelation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } = null;
    public string? Parent { get; set; } = null;
    public List<string>? Child { get; set; } = null;
}