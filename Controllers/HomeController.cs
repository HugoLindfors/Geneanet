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

    // GET: Members
    public IActionResult Index()
    {
        if (documents == null) { return NotFound(); }

        foreach(BsonDocument document in documents)
        {
            //Console.WriteLine(document.ToString());

            Member deptObj = BsonSerializer.Deserialize<Member>(document);

            Console.WriteLine($"{deptObj.FirstName} {deptObj.LastName}");
        }

        return View(documents);
    }

    // GET: Members/Details/:id
    public IActionResult Details(string? id)
    {
        if (id == null) { return NotFound(); }
        return View();
    }

    // GET: Members/Create
    public IActionResult Create()
    {
        return View();
    }

    // GET: Members/Edit/:id
    public IActionResult Edit(string? id)
    {
        if (id == null) { return NotFound(); }
        return View();
    }

    // GET: Members/Edit/:id
    public IActionResult Delete(string? id)
    {
        if (id == null) { return NotFound(); }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
