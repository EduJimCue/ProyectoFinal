using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaKarate.Migrations
{
    /// <inheritdoc />
    public partial class SizeStockChangeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 2, 22, 12, 15, 21, 254, DateTimeKind.Local).AddTicks(6496));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "SignUpDate",
                value: new DateTime(2024, 2, 22, 12, 15, 21, 254, DateTimeKind.Local).AddTicks(6195));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 2, 21, 19, 24, 10, 231, DateTimeKind.Local).AddTicks(2423));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "SizeId", "Stock" },
                values: new object[] { 1, 100 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "SignUpDate",
                value: new DateTime(2024, 2, 21, 19, 24, 10, 231, DateTimeKind.Local).AddTicks(2034));
        }
    }
}
