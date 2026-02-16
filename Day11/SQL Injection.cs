using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString =
            "Server=localhost\\SQLEXPRESS;Database=CustomerManagementDB;Trusted_Connection=True;TrustServerCertificate=True;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string userInput = "1 OR 1=1";

            string query = $"SELECT * FROM Customer WHERE CustomerId = {userInput}";

            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine(
                        $"Id: {reader["CustomerId"]}, " +
                        $"Name: {reader["CustomerName"]}");
                }
            }
        }
    }
}
