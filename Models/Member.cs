// To make MongoDB's ObjectID work in C# the following namespaces must be included
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// This namespace is included to improve the way some of the varaible names look on the frontend, like FirstName to "First Name"
using System.ComponentModel.DataAnnotations;

namespace Geneanet.Models;

public class Member
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } = null; // Initially every varaible is set to NULL

    [Display(Name = "Family Name")]
    public string? LastName { get; set; } = null;

    [Display(Name = "First Name")]
    public string? FirstName { get; set; } = null;

    [Display(Name = "Patronymic")]
    public string? Patronymic { get; set; } = null;

    [Display(Name = "Gender")]
    public string? Gender { get; set; } = null;

    [Display(Name = "Nationality")]
    public string? Nationality { get; set; } = null;

    [Display(Name = "Occupation")]
    public string? Occupation { get; set; } = null;

    [Display(Name = "Notes")]
    public string? Notes { get; set; } = null;
    
    [Display(Name = "Source")]
    public string? Source { get; set; } = null;
    public string? Child { get; set; } = null;
}