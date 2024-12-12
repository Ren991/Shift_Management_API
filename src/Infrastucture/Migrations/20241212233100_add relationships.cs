using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class addrelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shift_BarberShop_BarberShopId",
                table: "Shift");

            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Users_BarberId",
                table: "Shift");

            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Users_ClientId",
                table: "Shift");

            migrationBuilder.DropIndex(
                name: "IX_Shift_ClientId",
                table: "Shift");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Shift",
                newName: "ClientID");

            migrationBuilder.RenameColumn(
                name: "BarberShopId",
                table: "Shift",
                newName: "BarberShopID");

            migrationBuilder.RenameColumn(
                name: "BarberId",
                table: "Shift",
                newName: "BarberID");

            migrationBuilder.RenameIndex(
                name: "IX_Shift_BarberShopId",
                table: "Shift",
                newName: "IX_Shift_BarberShopID");

            migrationBuilder.RenameIndex(
                name: "IX_Shift_BarberId",
                table: "Shift",
                newName: "IX_Shift_BarberID");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_BarberShop_BarberShopID",
                table: "Shift",
                column: "BarberShopID",
                principalTable: "BarberShop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_Users_BarberID",
                table: "Shift",
                column: "BarberID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shift_BarberShop_BarberShopID",
                table: "Shift");

            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Users_BarberID",
                table: "Shift");

            migrationBuilder.RenameColumn(
                name: "ClientID",
                table: "Shift",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "BarberShopID",
                table: "Shift",
                newName: "BarberShopId");

            migrationBuilder.RenameColumn(
                name: "BarberID",
                table: "Shift",
                newName: "BarberId");

            migrationBuilder.RenameIndex(
                name: "IX_Shift_BarberShopID",
                table: "Shift",
                newName: "IX_Shift_BarberShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Shift_BarberID",
                table: "Shift",
                newName: "IX_Shift_BarberId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_ClientId",
                table: "Shift",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_BarberShop_BarberShopId",
                table: "Shift",
                column: "BarberShopId",
                principalTable: "BarberShop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_Users_BarberId",
                table: "Shift",
                column: "BarberId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_Users_ClientId",
                table: "Shift",
                column: "ClientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
