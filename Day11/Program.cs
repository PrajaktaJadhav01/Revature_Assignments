using System;
using System.Data.SqlClient;

namespace Day11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString =
                "Server=localhost\\SQLEXPRESS;Database=TestDB;Trusted_Connection=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connection Opened Successfully");

                    string query = "SELECT Id, Age, Name FROM Customers WHERE Age > 25";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        int age = reader.GetInt32(1);
                        string name = reader.GetString(2);

                        Console.WriteLine($"{id}\t{age}\t{name}");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
