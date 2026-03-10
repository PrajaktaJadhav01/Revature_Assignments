using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

// Create MongoDB client
var client = new MongoClient("mongodb://localhost:27017");

// Get database
var database = client.GetDatabase("testdb");

Console.WriteLine("Connection successful!");

// Get collection
var collection = database.GetCollection<Customer>("customers");

// Insert customer
var customer = new Customer
{
    Name = "Prajakta Jadhav",
    Age = 22,
    Email = "prajaktajadhav@gmail.com"
};

collection.InsertOne(customer);

Console.WriteLine("Customer inserted successfully!");

// Read customers
var customers = collection.Find(_ => true).ToList();

foreach (var c in customers)
{
    Console.WriteLine($"Name: {c.Name}, Age: {c.Age}, Email: {c.Email}");
}


// Customer Model
class Customer
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = "";

    public string Name { get; set; } = "";
    public int Age { get; set; }
    public string Email { get; set; } = "";
}