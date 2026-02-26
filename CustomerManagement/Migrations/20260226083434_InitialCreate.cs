using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerManagement.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Customer table already exists in database
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // No rollback needed
        }
    }
}