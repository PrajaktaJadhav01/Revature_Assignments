using System;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleAppPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            // Connection string for SQL Server Express
            string connectionString =
                "Server=localhost\\SQLEXPRESS;Database=CustomerManagementDB;Trusted_Connection=True;TrustServerCertificate=True;";

            // Creating SqlConnection object
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                // Opening connection
                connection.Open();
                Console.WriteLine("Connection opened successfully.\n");

                // Calling different ADO.NET demos
                ExecuteReaderDemo(connection);
                ExecuteScalarDemo(connection);
                InsertDemo(connection);
                ParameterizedQueryDemo(connection);
                SqlDataAdapterDemo(connection);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                // Closing connection
                connection.Close();
            }

            Console.ReadLine();
        }

        // -------------------------------------------------
        // ExecuteReader Demo (Read multiple rows)
        // -------------------------------------------------
        static void ExecuteReaderDemo(SqlConnection connection)
        {
            Console.WriteLine("---- ExecuteReader Demo ----");

            string query = "SELECT CustomerId, CustomerName, Email FROM Customer";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(
                    "Id: " + reader["CustomerId"] +
                    ", Name: " + reader["CustomerName"] +
                    ", Email: " + reader["Email"]);
            }

            reader.Close();
            Console.WriteLine();
        }

        // -------------------------------------------------
        // ExecuteScalar Demo (Returns single value)
        // -------------------------------------------------
        static void ExecuteScalarDemo(SqlConnection connection)
        {
            Console.WriteLine("---- ExecuteScalar Demo ----");

            string query = "SELECT COUNT(*) FROM Customer";

            SqlCommand command = new SqlCommand(query, connection);

            int count = (int)command.ExecuteScalar();

            Console.WriteLine("Total Customers: " + count);
            Console.WriteLine();
        }

        // -------------------------------------------------
        // ExecuteNonQuery Demo (Insert Data)
        // -------------------------------------------------
        static void InsertDemo(SqlConnection connection)
        {
            Console.WriteLine("---- ExecuteNonQuery (Insert) Demo ----");

            string query = @"INSERT INTO Customer
                             (CustomerName, Email, Phone, SegmentId)
                             VALUES (@Name, @Email, @Phone, 1)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Name", "Practice User");
            command.Parameters.AddWithValue("@Email", "practice@email.com");
            command.Parameters.AddWithValue("@Phone", "8888888888");

            int rows = command.ExecuteNonQuery();

            Console.WriteLine("Rows inserted: " + rows);
            Console.WriteLine();
        }

        // -------------------------------------------------
        // Parameterized Query Demo (Prevents SQL Injection)
        // -------------------------------------------------
        static void ParameterizedQueryDemo(SqlConnection connection)
        {
            Console.WriteLine("---- Parameterized Query Demo ----");

            string query =
                "SELECT CustomerId, CustomerName FROM Customer WHERE CustomerName LIKE @Name";

            SqlCommand command = new SqlCommand(query, connection);

            string searchName = "Prajakta";

            // Adding parameter safely
            command.Parameters.AddWithValue("@Name", "%" + searchName + "%");

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(
                    "Id: " + reader["CustomerId"] +
                    ", Name: " + reader["CustomerName"]);
            }

            reader.Close();
            Console.WriteLine();
        }

        // -------------------------------------------------
        // SqlDataAdapter Demo (Disconnected Architecture)
        // -------------------------------------------------
        static void SqlDataAdapterDemo(SqlConnection connection)
        {
            Console.WriteLine("---- SqlDataAdapter Demo ----");

            string query = "SELECT CustomerId, CustomerName FROM Customer";

            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

            DataTable table = new DataTable();

            // Filling DataTable (Disconnected mode)
            adapter.Fill(table);

            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine(
                    "Id: " + row["CustomerId"] +
                    ", Name: " + row["CustomerName"]);
            }

            Console.WriteLine();
        }
    }
}
