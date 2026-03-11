using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.AdminService.Migrations
{
    
    public partial class FixAdminPassword : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$WkJhS7DVLiCdKGHu3XJXOeNWVyJpnFBIAUO7hznKjsOtJ1EZerTHm");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$ZsYvQn7PjmqPDev9wHDiEOJVqZLcwTGV9R1KzFHLxMfVXJHQAkF7i");
        }
    }
}
