using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class Db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicesAndHaircuts_Shift_ShiftId",
                table: "ServicesAndHaircuts");

            migrationBuilder.AlterColumn<int>(
                name: "ShiftId",
                table: "ServicesAndHaircuts",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

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

            migrationBuilder.AlterColumn<int>(
                name: "ShiftId",
                table: "ServicesAndHaircuts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesAndHaircuts_Shift_ShiftId",
                table: "ServicesAndHaircuts",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
