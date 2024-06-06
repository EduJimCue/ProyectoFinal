using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaKarate.Migrations
{
    /// <inheritdoc />
    public partial class DescriptionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 4, 2, 19, 35, 32, 261, DateTimeKind.Local).AddTicks(7490));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Recuerda tus mejores momentos de competion con este kimono que refleja los valores de tus años dorados");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "SignUpDate",
                value: new DateTime(2024, 4, 2, 19, 35, 32, 261, DateTimeKind.Local).AddTicks(7171));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 3, 13, 20, 26, 0, 305, DateTimeKind.Local).AddTicks(5289));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "SignUpDate",
                value: new DateTime(2024, 3, 13, 20, 26, 0, 305, DateTimeKind.Local).AddTicks(5003));
        }
    }
}
