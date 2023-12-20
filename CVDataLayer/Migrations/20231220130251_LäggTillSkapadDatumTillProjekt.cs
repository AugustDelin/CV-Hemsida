using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class LäggTillSkapadDatumTillProjekt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8a1b32f0-a029-46b5-b519-f7897adc2839");

            migrationBuilder.AddColumn<DateTime>(
                name: "SkapadDatum",
                table: "Projekt",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Aktiv", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Profilbild", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ca7b7202-3f38-4f4f-bb83-d4b5dfe94e00", 0, false, "28a56d16-ed26-4732-b66b-7278684a7a69", null, false, false, null, null, null, null, null, false, null, "ae8d8844-37e4-40e1-8b2b-bf6abeff1790", false, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca7b7202-3f38-4f4f-bb83-d4b5dfe94e00");

            migrationBuilder.DropColumn(
                name: "SkapadDatum",
                table: "Projekt");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Aktiv", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Profilbild", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8a1b32f0-a029-46b5-b519-f7897adc2839", 0, false, "d406313a-dbd6-49db-a0d0-0977e64c8c94", null, false, false, null, null, null, null, null, false, null, "fc94a56c-ed4c-4455-85c2-445e1f415592", false, null });
        }
    }
}
