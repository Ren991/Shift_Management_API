using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class createadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IsActive", "LastName", "Password", "Role" },
                values: new object[] { 10, "admin@example.com", "Admin", true, "User", "123321", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IsActive", "LastName", "Password", "Role" },
                values: new object[] { 1, "admin@example.com", "Admin", true, "User", "123321", 0 });
        }
    }
}
