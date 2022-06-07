using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vitatic.DataAccess.Migrations
{
    public partial class Vitatic5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_Interfaces_InterfaceId",
                table: "Instructions");

            migrationBuilder.DropIndex(
                name: "IX_Instructions_InterfaceId",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "InterfaceId",
                table: "Instructions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InterfaceId",
                table: "Instructions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_InterfaceId",
                table: "Instructions",
                column: "InterfaceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_Interfaces_InterfaceId",
                table: "Instructions",
                column: "InterfaceId",
                principalTable: "Interfaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
