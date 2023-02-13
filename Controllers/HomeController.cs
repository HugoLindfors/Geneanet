using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Geneanet.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Geneanet.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    MongoClient dbClient = new MongoClient("mongodb://localhost:27017"); // Connect to MongoDB client

    List<BsonElement>? members = null;

    // GET: Members
    public IActionResult Index()
    {
        members = dbClient
            .GetDatabase("GenealogyDB")
            .GetCollection<BsonDocument>("Members")
            .Find(new BsonDocument())
            .FirstOrDefault()
            .ToList();
            
        foreach (var member in members)
        {
            Console.WriteLine(member);
        }
        return View(members);
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
