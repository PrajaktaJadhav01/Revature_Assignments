using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using WebApiProject.DTOs;

namespace WebApiProject.Tests
{
    public class IntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient client;

        public IntegrationTest(WebApplicationFactory<Program> factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_Customers_Returns_OK()
        {
            // Act
            var response = await client.GetAsync("/api/customer");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post_Customers_Returns_OK()
        {
            // Arrange
            var customer = new CustomerDTO
            {
                Name = "Test Customer",
                Email = "test@email.com"
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/customer", customer);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
