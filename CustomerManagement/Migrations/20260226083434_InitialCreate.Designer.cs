using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerManagement.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Customer table already exists in the database
            // So we are not creating it again
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // No rollback needed
        }
    }
}