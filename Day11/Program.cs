using System;
using Microsoft.Data.SqlClient;

namespace Day11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString =
                "Server=localhost\\SQLEXPRESS;Database=CustomerManagementDB;Trusted_Connection=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connected Successfully!\n");

                    string query = @"
                        SELECT 
                            C.CustomerName,
                            CP.Name AS ContactPerson,
                            CA.City,
                            CI.InteractionType,
                            CI.Subject
                        FROM Customer C
                        LEFT JOIN ContactPerson CP ON C.CustomerId = CP.CustomerId
                        LEFT JOIN CustomerAddress CA ON C.CustomerId = CA.CustomerId
                        LEFT JOIN CustomerInteraction CI ON C.CustomerId = CI.CustomerId";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string customerName = reader["CustomerName"].ToString();
                        string contactPerson = reader["ContactPerson"]?.ToString();
                        string city = reader["City"]?.ToString();
                        string interactionType = reader["InteractionType"]?.ToString();
                        string subject = reader["Subject"]?.ToString();

                        Console.WriteLine(
                            $"Customer: {customerName} | Contact: {contactPerson} | City: {city} | Type: {interactionType} | Subject: {subject}");
                    }

                    reader.Close();

                    Console.WriteLine("\n--- Active Customers ---");

                    SqlCommand spCommand = new SqlCommand("GetActiveCustomers", connection);
                    spCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader spReader = spCommand.ExecuteReader();

                    while (spReader.Read())
                    {
                        Console.WriteLine(spReader["CustomerName"]);
                    }

                    spReader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
