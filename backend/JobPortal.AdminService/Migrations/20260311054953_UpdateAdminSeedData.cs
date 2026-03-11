using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.AdminService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdminSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Email", "Password" },
                values: new object[] { "admin@gmail.com", "$2a$11$QHrwvsquG9jFPQZ7oZ71Mue9GFeX58yg4m7dPG7fpqS8wAvbx6lV2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Email", "Password" },
                values: new object[] { "admin@jobportal.com", "$2a$11$WkJhS7DVLiCdKGHu3XJXOeNWVyJpnFBIAUO7hznKjsOtJ1EZerTHm" });
        }
    }
}
