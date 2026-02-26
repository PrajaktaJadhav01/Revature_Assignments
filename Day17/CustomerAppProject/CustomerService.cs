using System.Data.SqlClient;

namespace CustomerAppProject
{
    public class CustomerService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public CustomerService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection")!;
        }

        // GET ALL
        public async Task<List<Customer>> GetAllCustomersAsync(string? name)
        {
            List<Customer> customers = new();

            using SqlConnection conn = new(_connectionString);

            string query = "SELECT CustomerId, CustomerName, Email FROM Customer";

            if (!string.IsNullOrEmpty(name))
                query += " WHERE CustomerName LIKE @name";

            SqlCommand cmd = new(query, conn);

            if (!string.IsNullOrEmpty(name))
                cmd.Parameters.AddWithValue("@name", $"%{name}%");

            await conn.OpenAsync();

            SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                customers.Add(new Customer
                {
                    CustomerId = Convert.ToInt32(reader["CustomerId"]),
                    CustomerName = reader["CustomerName"].ToString()!,
                    Email = reader["Email"].ToString()!
                });
            }

            return customers;
        }

        // GET BY ID
        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            using SqlConnection conn = new(_connectionString);

            string query = "SELECT CustomerId, CustomerName, Email FROM Customer WHERE CustomerId=@id";

            SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            await conn.OpenAsync();

            SqlDataReader reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Customer
                {
                    CustomerId = Convert.ToInt32(reader["CustomerId"]),
                    CustomerName = reader["CustomerName"].ToString()!,
                    Email = reader["Email"].ToString()!
                };
            }

            return null;
        }

        // POST
        public async Task AddCustomerAsync(Customer customer)
        {
            using SqlConnection conn = new(_connectionString);

            string query = "INSERT INTO Customer (CustomerName, Email) VALUES (@name,@email)";

            SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@name", customer.CustomerName);
            cmd.Parameters.AddWithValue("@email", customer.Email);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        // PUT (Full Update)
        public async Task UpdateCustomerAsync(Customer customer)
        {
            using SqlConnection conn = new(_connectionString);

            string query = "UPDATE Customer SET CustomerName=@name, Email=@email WHERE CustomerId=@id";

            SqlCommand cmd = new(query, conn);

            cmd.Parameters.AddWithValue("@id", customer.CustomerId);
            cmd.Parameters.AddWithValue("@name", customer.CustomerName);
            cmd.Parameters.AddWithValue("@email", customer.Email);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        // PATCH (Partial Update)
        public async Task PatchCustomerEmailAsync(int id, string email)
        {
            using SqlConnection conn = new(_connectionString);

            string query = "UPDATE Customer SET Email=@email WHERE CustomerId=@id";

            SqlCommand cmd = new(query, conn);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@email", email);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        // DELETE
        public async Task DeleteCustomerAsync(int id)
        {
            using SqlConnection conn = new(_connectionString);

            string query = "DELETE FROM Customer WHERE CustomerId=@id";

            SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}