using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaKarate.Migrations
{
    /// <inheritdoc />
    public partial class ImagesName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "katategiAdidas");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 5, 22, 18, 18, 22, 300, DateTimeKind.Local).AddTicks(1436));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "SignUpDate",
                value: new DateTime(2024, 5, 22, 18, 18, 22, 300, DateTimeKind.Local).AddTicks(1131));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Images");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 5, 16, 13, 15, 24, 524, DateTimeKind.Local).AddTicks(9370));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "SignUpDate",
                value: new DateTime(2024, 5, 16, 13, 15, 24, 524, DateTimeKind.Local).AddTicks(9074));
        }
    }
}
