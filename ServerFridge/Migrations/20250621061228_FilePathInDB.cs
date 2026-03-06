using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerFridge.Migrations
{
    /// <inheritdoc />
    public partial class FilePathInDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image Path",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("1605d6a2-5e70-44ed-9393-bccb8e46b910"),
                column: "Image Path",
                value:null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("20fadc8c-7b02-4668-b652-073bcde750fc"),
                column: "Image Path",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("9b19425c-2503-48cb-b823-8a123b3a8ce3"),
                column: "Image Path",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image Path",
                table: "Products");
        }
    }
}
