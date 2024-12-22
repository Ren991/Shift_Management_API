using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class shiftadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShiftId",
                table: "ServicesAndHaircuts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    Confirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPayabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    BarberId = table.Column<int>(type: "INTEGER", nullable: false),
                    BarberShopId = table.Column<int>(type: "INTEGER", nullable: false),
                    Day = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ShiftTime = table.Column<TimeOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shift_BarberShop_BarberShopId",
                        column: x => x.BarberShopId,
                        principalTable: "BarberShop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shift_Users_BarberId",
                        column: x => x.BarberId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shift_Users_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServicesAndHaircuts_ShiftId",
                table: "ServicesAndHaircuts",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_BarberId",
                table: "Shift",
                column: "BarberId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_BarberShopId",
                table: "Shift",
                column: "BarberShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_ClientId",
                table: "Shift",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesAndHaircuts_Shift_ShiftId",
                table: "ServicesAndHaircuts",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicesAndHaircuts_Shift_ShiftId",
                table: "ServicesAndHaircuts");

            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropIndex(
                name: "IX_ServicesAndHaircuts_ShiftId",
                table: "ServicesAndHaircuts");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "ServicesAndHaircuts");
        }
    }
}
