using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Geneanet.Models;

public enum Gender { Male, Female };

public class Member
{
    public string? LastName { get; set; } = null;
    public string? FirstName { get; set; } = null;
    public string? Patronymic { get; set; } = null;
    public Gender? Gender { get; set; } = null;
    public string? Nationality { get; set; } = null;
    public string? Occupation { get; set; } = null;
    public string? Notes { get; set; } = null;
    public string? Source { get; set; } = null;
}