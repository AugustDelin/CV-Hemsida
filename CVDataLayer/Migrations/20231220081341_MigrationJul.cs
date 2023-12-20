using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class MigrationJul : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eea62ae5-64d1-44c3-9c38-d850babbbbee");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Aktiv", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Profilbild", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7b44b3d6-11e7-4973-bfb2-bada9c6d5179", 0, false, "3cfa005a-fd4a-4ad8-b6ec-3dad305b41fc", null, false, false, null, null, null, null, null, false, null, "15974585-bb15-4c21-a17a-e132a46cf99f", false, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b44b3d6-11e7-4973-bfb2-bada9c6d5179");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Aktiv", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Profilbild", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "eea62ae5-64d1-44c3-9c38-d850babbbbee", 0, false, "29c06ac7-a5d8-429f-a3ce-861c86cbe086", null, false, false, null, null, null, null, null, false, null, "0bf675f2-26f0-4bd3-8e81-bf29e011c69a", false, null });
        }
    }
}
