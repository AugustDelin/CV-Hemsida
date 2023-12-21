using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UppdateraProjektPrimärnyckel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjektId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProjektId",
                table: "AspNetUsers",
                column: "ProjektId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Projekt_ProjektId",
                table: "AspNetUsers",
                column: "ProjektId",
                principalTable: "Projekt",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Projekt_ProjektId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProjektId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProjektId",
                table: "AspNetUsers");
        }
    }
}
