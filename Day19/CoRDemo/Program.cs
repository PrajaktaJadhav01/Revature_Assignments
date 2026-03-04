using System;
using System.Text;
using System.Data.SqlClient;

// Message
var message = "How are you?";

// create handlers
var encryption = new RsaEncryption();
var archival = new Archival();
var email = new Email();
var pdf = new Pdf();
var cloud = new CloudSave();

// chain them
encryption.SetNext(archival);
archival.SetNext(email);
email.SetNext(pdf);
pdf.SetNext(cloud);

// start chain
encryption.Handle(message);

interface IHandler
{
    void SetNext(IHandler handler);
    void Handle(string message);
}

abstract class HandlerBase : IHandler
{
    private IHandler next;

    public void SetNext(IHandler handler)
    {
        next = handler;
    }

    public virtual void Handle(string message)
    {
        if (next != null)
        {
            next.Handle(message);
        }
    }
}

// Encryption Handler
class RsaEncryption : HandlerBase
{
    public override void Handle(string message)
    {
        StringBuilder encryptedMessage = new StringBuilder();

        foreach (var ch in message)
        {
            encryptedMessage.Append((char)(ch + 1));
        }

        Console.WriteLine($"Encrypted Message: {encryptedMessage}");

        base.Handle(encryptedMessage.ToString());
    }
}

// Archival Handler
class Archival : HandlerBase
{
    public override void Handle(string message)
    {
        Console.WriteLine("Archiving message...");

        // Save in database
        string connectionString =
        @"Server=(localdb)\MSSQLLocalDB;Database=CustomerManagementDB;Trusted_Connection=True;";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            string query = "INSERT INTO Messages (Content) VALUES (@msg)";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@msg", message);

            try
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("Message saved to database.");
            }
            catch
            {
                Console.WriteLine("Database table 'Messages' not found.");
            }
        }

        base.Handle(message);
    }
}

// Email Handler
class Email : HandlerBase
{
    public override void Handle(string message)
    {
        Console.WriteLine($"Sending Email: {message}");

        base.Handle(message);
    }
}

// PDF Handler
class Pdf : HandlerBase
{
    public override void Handle(string message)
    {
        Console.WriteLine($"Generating PDF for message: {message}");

        base.Handle(message);
    }
}

// Cloud Save Handler
class CloudSave : HandlerBase
{
    public override void Handle(string message)
    {
        Console.WriteLine($"Saving message to Cloud: {message}");

        base.Handle(message);
    }
}