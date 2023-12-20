using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class DeltarProjekt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca7b7202-3f38-4f4f-bb83-d4b5dfe94e00");

            migrationBuilder.AddColumn<int>(
                name: "CVId",
                table: "PersonDeltarProjekt",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonDeltarProjekt_CVId",
                table: "PersonDeltarProjekt",
                column: "CVId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonDeltarProjekt_CVs_CVId",
                table: "PersonDeltarProjekt",
                column: "CVId",
                principalTable: "CVs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonDeltarProjekt_CVs_CVId",
                table: "PersonDeltarProjekt");

            migrationBuilder.DropIndex(
                name: "IX_PersonDeltarProjekt_CVId",
                table: "PersonDeltarProjekt");

            migrationBuilder.DropColumn(
                name: "CVId",
                table: "PersonDeltarProjekt");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Aktiv", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Profilbild", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ca7b7202-3f38-4f4f-bb83-d4b5dfe94e00", 0, false, "28a56d16-ed26-4732-b66b-7278684a7a69", null, false, false, null, null, null, null, null, false, null, "ae8d8844-37e4-40e1-8b2b-bf6abeff1790", false, null });
        }
    }
}
