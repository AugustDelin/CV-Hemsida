using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CVs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kompetenser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Utbildningar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TidigareErfarenhet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilbildPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnvändarId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CVs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CVs_users_AnvändarId",
                        column: x => x.AnvändarId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personer",
                columns: table => new
                {
                    Personnummer = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Förnamn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Efternamn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnvändarID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personer", x => x.Personnummer);
                    table.ForeignKey(
                        name: "FK_Personer_users_AnvändarID",
                        column: x => x.AnvändarID,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projekts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Beskrivning = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnvändarId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projekts_users_AnvändarId",
                        column: x => x.AnvändarId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "AccessFailedCount", "Active", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f64bfb6e-e58a-4edd-b3d0-4ce65e5a383b", 0, false, "556d2d4f-ca3a-460c-ace8-17ba20b5fdf9", null, false, false, null, null, null, "granlunda", null, null, false, "7e9a7be6-1788-4d95-bbc0-ea0218ae22fd", false, "erik.alingsas@gmail.com" });

            migrationBuilder.CreateIndex(
                name: "IX_CVs_AnvändarId",
                table: "CVs",
                column: "AnvändarId");

            migrationBuilder.CreateIndex(
                name: "IX_Personer_AnvändarID",
                table: "Personer",
                column: "AnvändarID");

            migrationBuilder.CreateIndex(
                name: "IX_Projekts_AnvändarId",
                table: "Projekts",
                column: "AnvändarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CVs");

            migrationBuilder.DropTable(
                name: "Personer");

            migrationBuilder.DropTable(
                name: "Projekts");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
