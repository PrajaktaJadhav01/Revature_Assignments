using System;
using Microsoft.Data.SqlClient;

namespace Day11
{
    class Program
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
                    Console.WriteLine("Connected Successfully!");

                    string query = "SELECT CustomerId, CustomerName FROM Customer";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(
                                reader["CustomerId"] + " - " +
                                reader["CustomerName"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
