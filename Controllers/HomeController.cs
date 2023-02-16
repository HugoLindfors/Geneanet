using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Geneanet.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Geneanet.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
    static MongoClient dbClient = new MongoClient("mongodb://localhost:27017"); // Connect to the MongoDB client
    static IMongoDatabase database = dbClient.GetDatabase("GenealogyDB"); // Connect to the database
    static IMongoCollection<BsonDocument>? collection = database.GetCollection<BsonDocument>("Members"); // Connect to the Members collection
    static List<BsonDocument>? documents = collection.Find(new BsonDocument()).ToList(); // Convert the collection to a list of BsonDocuments
    static List<Member> members = new List<Member>(); // Creates member list

    // GET: Members
    public IActionResult Index()
    {
        documents = collection.Find(new BsonDocument()).ToList(); // Convert the collection to a list of BsonDocuments

        if (documents == null)
            return NotFound();

        members.Clear();

        foreach(BsonDocument document in documents)
        {
            Member member = BsonSerializer.Deserialize<Member>(document);
            members.Add(member);
        }

        return View(members);
    }

    // GET: Members/Details/:id
    public IActionResult Details(string? id)
    {
        documents = collection.Find(new BsonDocument()).ToList(); // Convert the collection to a list of BsonDocuments

        if (documents == null)
            return NotFound();

        else if (id == null)
            return NotFound();

        members.Clear();

        foreach(BsonDocument document in documents)
        {
            Member member = BsonSerializer.Deserialize<Member>(document);
            
            if (member.Id == id)
            {
                return View(member);
            }
        }

        return NotFound();
    }

    // GET: Members/Create
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,LastName,FirstName,Patronymic,Gender,Nationality,Occupation,Notes,Source")] Member member)
    {
        documents = collection.Find(new BsonDocument()).ToList(); // Convert the collection to a list of BsonDocuments

        if (member.LastName == null)
            return RedirectToAction(nameof(Index));

        if (member.FirstName == null)
            return RedirectToAction(nameof(Index));
        
        if (member.Patronymic == null)
            member.Patronymic = "";

        if (member.Gender == null)
            return RedirectToAction(nameof(Index));

        if (member.Nationality == null)
            member.Nationality = "";

        if (member.Occupation == null)
            member.Occupation = "";

        if (member.Notes == null)
            member.Notes = "";

        if (member.Source == null)
            member.Source = "";

        if (ModelState.IsValid)
        {
            Console.WriteLine(member.LastName);
            BsonDocument document = new BsonDocument
            {
                { "LastName", member.LastName },
                { "FirstName", member.FirstName },
                { "Patronymic", member.Patronymic },
                { "Gender", member.Gender },
                { "Nationality", member.Nationality },
                { "Occupation", member.Occupation },
                { "Notes", member.Notes },
                { "Source", member.Source }
            };
            
            if (collection != null)
                collection.InsertOne(document);

            else
                Console.WriteLine("Fail! Collection is NULL!");

            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    // GET: Members/Edit/:id
    public IActionResult Edit(string? id)
    {
        if (id == null)
            return NotFound();

        return View();
    }

    // GET: Members/Edit/:id
    public IActionResult Delete(string? id)
    {
        if (id == null)
            return NotFound();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
