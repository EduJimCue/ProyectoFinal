using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaKarate.Migrations
{
    /// <inheritdoc />
    public partial class SizeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "SizeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "SignUpDate",
                value: new DateTime(2024, 2, 12, 13, 28, 5, 33, DateTimeKind.Local).AddTicks(7682));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "SignUpDate",
                value: new DateTime(2024, 2, 8, 20, 8, 25, 122, DateTimeKind.Local).AddTicks(36));
        }
    }
}
