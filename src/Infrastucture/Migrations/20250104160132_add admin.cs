using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class addadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Users_BarberID",
                table: "Shift");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                column: "Email",
                value: "admin@mati.com");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_Users_BarberID",
                table: "Shift",
                column: "BarberID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Users_BarberID",
                table: "Shift");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                column: "Email",
                value: "admin@example.com");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_Users_BarberID",
                table: "Shift",
                column: "BarberID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
