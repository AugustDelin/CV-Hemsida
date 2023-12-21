using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class TaBortSkapadDatumFrånProjekt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SkapadDatum",
                table: "Projekt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SkapadDatum",
                table: "Projekt",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
