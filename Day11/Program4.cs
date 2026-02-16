using System;
using System.Data.SqlClient;

namespace ConsoleApp14
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
                    Console.WriteLine("Connection opened successfully.");

                    ParameterizedQueryDemo(connection);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            Console.ReadLine();
        }

        static void ParameterizedQueryDemo(SqlConnection connection)
        {
            string query = "SELECT CustomerId, CustomerName, Email, Phone FROM Customer WHERE CustomerName LIKE @Name";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                string name = "Prajakta";   // change if you want

                command.Parameters.AddWithValue("@Name", "%" + name + "%");

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Id: " + reader["CustomerId"] +
                                              ", Name: " + reader["CustomerName"] +
                                              ", Email: " + reader["Email"] +
                                              ", Phone: " + reader["Phone"]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No customer found.");
                    }
                }
            }
        }
    }
}