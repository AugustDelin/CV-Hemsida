using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: "12571621-dc37-4066-81e7-bbc9616d1127");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "AccessFailedCount", "Aktiv", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Profilbild", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e95907c7-482d-4f65-ba3d-365d15c32e9d", 0, false, "6d2c6fc5-b74c-454f-a9cc-d28822354d43", null, false, false, null, null, null, null, null, false, null, "14acceb6-6b91-472a-b37d-7cce9329a1d1", false, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: "e95907c7-482d-4f65-ba3d-365d15c32e9d");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "AccessFailedCount", "Aktiv", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Profilbild", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "12571621-dc37-4066-81e7-bbc9616d1127", 0, false, "a31a6bcf-85de-4b0e-887a-f9faf2705ca8", null, false, false, null, null, null, null, null, false, null, "86a2f37e-8495-41a1-9c5f-d853a33a6553", false, null });
        }
    }
}
