using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Data;
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
                    Console.WriteLine("Connected Successfully!\n");

                    SqlDataAdapterDemo(connection);
                    ExecuteScalarDemo(connection);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            Console.ReadLine();
        }

        // 🔹 USING SQL DATA ADAPTER
        static void SqlDataAdapterDemo(SqlConnection connection)
        {
            string query = "SELECT * FROM Customer";

            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);

                Console.WriteLine("---- Customer List ----\n");

                foreach (DataRow row in table.Rows)
                {
                    Console.WriteLine(
                        $"ID: {row["CustomerId"]}, " +
                        $"Name: {row["CustomerName"]}, " +
                        $"Email: {row["Email"]}");
                }

                Console.WriteLine();
            }
        }

        // 🔹 USING EXECUTE SCALAR
        static void ExecuteScalarDemo(SqlConnection connection)
        {
            string query = "SELECT COUNT(*) FROM Customer";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                int count = (int)command.ExecuteScalar();
                Console.WriteLine($"Total Customers: {count}");
            }
        }
    }
}
