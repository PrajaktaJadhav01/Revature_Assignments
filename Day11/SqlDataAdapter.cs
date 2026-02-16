using System;
using System.Data;
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

            string query = "SELECT * FROM Customer";

            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);

                foreach (DataRow row in table.Rows)
                {
                    Console.WriteLine(
                        $"Id: {row["CustomerId"]}, " +
                        $"Name: {row["CustomerName"]}");
                }
            }
        }
    }
}
