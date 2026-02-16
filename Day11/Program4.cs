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

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Connection opened successfully.\n");

                // Call method
                ExecuteReaderDemo(connection);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            Console.ReadLine();
        }

        static void ExecuteReaderDemo(SqlConnection connection)
        {
            string query = "SELECT CustomerId, CustomerName, Email, Phone FROM Customer";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(
                    "Id: " + reader["CustomerId"] +
                    ", Name: " + reader["CustomerName"] +
                    ", Email: " + reader["Email"] +
                    ", Phone: " + reader["Phone"]);
            }

            reader.Close();
        }
    }
}
