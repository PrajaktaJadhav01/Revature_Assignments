using System.Data.SqlClient;

namespace CustomerAppProject
{
    public class CustomerService
    {
        private readonly string _connectionString;

        public CustomerService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        // GET ALL
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            List<Customer> customers = new List<Customer>();

            using SqlConnection conn = new SqlConnection(_connectionString);

            string query = "SELECT CustomerId, CustomerName, Email FROM Customer";

            SqlCommand cmd = new SqlCommand(query, conn);

            await conn.OpenAsync();

            SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                customers.Add(new Customer
                {
                    CustomerId = Convert.ToInt32(reader["CustomerId"]),
                    CustomerName = reader["CustomerName"].ToString(),
                    Email = reader["Email"].ToString()
                });
            }

            return customers;
        }

        // GET BY ID
        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            string query = "SELECT CustomerId, CustomerName, Email FROM Customer WHERE CustomerId=@id";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            await conn.OpenAsync();

            SqlDataReader reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Customer
                {
                    CustomerId = Convert.ToInt32(reader["CustomerId"]),
                    CustomerName = reader["CustomerName"].ToString(),
                    Email = reader["Email"].ToString()
                };
            }

            return null;
        }

        // POST
        public async Task AddCustomerAsync(Customer customer)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            string query = "INSERT INTO Customer (CustomerName, Email) VALUES (@name, @email)";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@name", customer.CustomerName);
            cmd.Parameters.AddWithValue("@email", customer.Email);

            await conn.OpenAsync();

            await cmd.ExecuteNonQueryAsync();
        }

        // PUT
        public async Task UpdateCustomerAsync(int id, Customer customer)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            string query = "UPDATE Customer SET CustomerName=@name, Email=@email WHERE CustomerId=@id";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", customer.CustomerName);
            cmd.Parameters.AddWithValue("@email", customer.Email);

            await conn.OpenAsync();

            await cmd.ExecuteNonQueryAsync();
        }

        // DELETE
        public async Task DeleteCustomerAsync(int id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            string query = "DELETE FROM Customer WHERE CustomerId=@id";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@id", id);

            await conn.OpenAsync();

            await cmd.ExecuteNonQueryAsync();
        }
    }
}