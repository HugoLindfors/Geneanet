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

public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;

    public AdminController(ILogger<AdminController> logger)
    {
        _logger = logger;
    }

    static MongoClient dbClient = new MongoClient("mongodb://localhost:27017"); // Connect to the MongoDB client
    static IMongoDatabase database = dbClient.GetDatabase("GenealogyDB"); // Connect to the database

    static IMongoCollection<BsonDocument>? collection = database.GetCollection<BsonDocument>("Members"); // Connect to the Members collection
    static List<BsonDocument>? documents = collection.Find(new BsonDocument()).ToList(); // Convert the collection to a list of BsonDocuments

    static IMongoCollection<BsonDocument>? childrenCollection = database.GetCollection<BsonDocument>("Children"); // Connect to the Children collection
    static List<BsonDocument>? childrenDocuments = childrenCollection.Find(new BsonDocument()).ToList(); // Convert the childrenCollection to a list of BsonDocuments

    static List<Member> members = new List<Member>(); // Creates member list

    // GET: Members
    public IActionResult Index()
    {
        documents = collection.Find(new BsonDocument()).ToList(); // Convert the collection to a list of BsonDocuments

        if (documents == null)
            return NotFound();

        members.Clear();

        foreach (BsonDocument document in documents)
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

        foreach (BsonDocument document in documents)
        {
            Member member = BsonSerializer.Deserialize<Member>(document);

            if (member.Id == id)
            {

                foreach (BsonDocument childDoc in documents)
                {
                    Member child = BsonSerializer.Deserialize<Member>(childDoc);

                    if (child.Id == member.Child)
                    {
                        ViewBag.Child = child;
                        return View(member);
                    }

                }
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
        documents = collection.Find(new BsonDocument()).ToList(); // Convert the collection to a list of BsonDocuments

        if (documents == null)
            return NotFound();

        else if (id == null)
            return NotFound();

        members.Clear();

        foreach (BsonDocument document in documents)
        {
            Member member = BsonSerializer.Deserialize<Member>(document);

            if (member.Id == id)
            {
                return View(member);
            }
        }

        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([Bind("Id,LastName,FirstName,Patronymic,Gender,Nationality,Occupation,Notes,Source")] Member updatedMember)
    {
        documents = collection.Find(new BsonDocument()).ToList(); // Convert the collection to a list of BsonDocuments

        if (ModelState.IsValid)
        {
            foreach (BsonDocument document in documents)
            {
                Member oldMember = BsonSerializer.Deserialize<Member>(document);

                if (oldMember.Id == updatedMember.Id)
                {
                    Console.WriteLine(oldMember.Id + " == " + updatedMember.Id);

                    if (collection != null)
                    {
                        FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("LastName", $"{oldMember.LastName}");
                        UpdateDefinition<BsonDocument> update = Builders<BsonDocument>.Update.Set("LastName", $"{updatedMember.LastName}");
                        await collection.UpdateOneAsync(filter, update);

                        filter = Builders<BsonDocument>.Filter.Eq("FirstName", $"{oldMember.FirstName}");
                        update = Builders<BsonDocument>.Update.Set("FirstName", $"{updatedMember.FirstName}");
                        await collection.UpdateOneAsync(filter, update);

                        filter = Builders<BsonDocument>.Filter.Eq("Patronymic", $"{oldMember.Patronymic}");
                        update = Builders<BsonDocument>.Update.Set("Patronymic", $"{updatedMember.Patronymic}");
                        await collection.UpdateOneAsync(filter, update);

                        filter = Builders<BsonDocument>.Filter.Eq("Gender", $"{oldMember.Gender}");
                        update = Builders<BsonDocument>.Update.Set("Gender", $"{updatedMember.Gender}");
                        await collection.UpdateOneAsync(filter, update);

                        filter = Builders<BsonDocument>.Filter.Eq("Nationality", $"{oldMember.Nationality}");
                        update = Builders<BsonDocument>.Update.Set("Nationality", $"{updatedMember.Nationality}");
                        await collection.UpdateOneAsync(filter, update);

                        filter = Builders<BsonDocument>.Filter.Eq("Occupation", $"{oldMember.Occupation}");
                        update = Builders<BsonDocument>.Update.Set("Occupation", $"{updatedMember.Occupation}");
                        await collection.UpdateOneAsync(filter, update);

                        filter = Builders<BsonDocument>.Filter.Eq("Notes", $"{oldMember.Notes}");
                        update = Builders<BsonDocument>.Update.Set("Notes", $"{updatedMember.Notes}");
                        await collection.UpdateOneAsync(filter, update);

                        filter = Builders<BsonDocument>.Filter.Eq("Source", $"{oldMember.Source}");
                        update = Builders<BsonDocument>.Update.Set("Source", $"{updatedMember.Source}");
                        await collection.UpdateOneAsync(filter, update);

                        return RedirectToAction(nameof(Index));
                    }
                }
            }
        }

        return View();
    }

    // GET: Members/Delete/:id
    public IActionResult Delete(string? id)
    {
        documents = collection.Find(new BsonDocument()).ToList(); // Convert the collection to a list of BsonDocuments

        if (documents == null)
            return NotFound();

        else if (id == null)
            return NotFound();

        members.Clear();

        foreach (BsonDocument document in documents)
        {
            Member member = BsonSerializer.Deserialize<Member>(document);

            if (member.Id == id)
            {
                return View(member);
            }
        }

        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete([Bind("Id,LastName,FirstName,Patronymic,Gender,Nationality,Occupation,Notes,Source")] Member updatedMember)
    {
        documents = collection.Find(new BsonDocument()).ToList(); // Convert the collection to a list of BsonDocuments

        if (ModelState.IsValid)
        {
            foreach (BsonDocument document in documents)
            {
                Member oldMember = BsonSerializer.Deserialize<Member>(document);

                if (oldMember.Id == updatedMember.Id)
                {
                    Console.WriteLine(oldMember.Id + " == " + updatedMember.Id);

                    if (collection != null)
                    {
                        FilterDefinition<BsonDocument> deleteFilter = Builders<BsonDocument>.Filter.Eq("LastName", $"{oldMember.LastName}");
                        
                        collection.DeleteOne(deleteFilter);

                        return RedirectToAction(nameof(Index));
                    }
                }
            }
        }

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
