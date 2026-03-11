using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.AdminService.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$ZsYvQn7PjmqPDev9wHDiEOJVqZLcwTGV9R1KzFHLxMfVXJHQAkF7i");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$x/mWbyoX0FRpY/2oOfjg/ebv.ODncYA0y5cgEru3ntLZ9WMWhzjBm");
        }
    }
}
