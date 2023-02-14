using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Geneanet.Models;

public class Member
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? _id { get; set; } = null;
    public string? LastName { get; set; } = null;
    public string? FirstName { get; set; } = null;
    public string? Patronymic { get; set; } = null;
    public string? Gender { get; set; } = null;
    public string? Nationality { get; set; } = null;
    public string? Occupation { get; set; } = null;
    public string? Notes { get; set; } = null;
    public string? Source { get; set; } = null;
}