using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class dbcontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicesAndHaircuts_Shift_ShiftId",
                table: "ServicesAndHaircuts");

            migrationBuilder.DropIndex(
                name: "IX_ServicesAndHaircuts_ShiftId",
                table: "ServicesAndHaircuts");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "ServicesAndHaircuts");

            migrationBuilder.CreateTable(
                name: "ShiftServices",
                columns: table => new
                {
                    ShiftId = table.Column<int>(type: "INTEGER", nullable: false),
                    ServiceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftServices", x => new { x.ShiftId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_ShiftServices_ServicesAndHaircuts_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ServicesAndHaircuts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShiftServices_Shift_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShiftServices_ServiceId",
                table: "ShiftServices",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShiftServices");

            migrationBuilder.AddColumn<int>(
                name: "ShiftId",
                table: "ServicesAndHaircuts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServicesAndHaircuts_ShiftId",
                table: "ServicesAndHaircuts",
                column: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesAndHaircuts_Shift_ShiftId",
                table: "ServicesAndHaircuts",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "Id");
        }
    }
}
