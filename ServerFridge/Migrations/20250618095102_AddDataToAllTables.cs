using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServerFridge.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "DefaultQuantity", "Name" },
                values: new object[,]
                {
                    { new Guid("1605d6a2-5e70-44ed-9393-bccb8e46b910"), 5, "Banana" },
                    { new Guid("20fadc8c-7b02-4668-b652-073bcde750fc"), 7, "Apple" },
                    { new Guid("9b19425c-2503-48cb-b823-8a123b3a8ce3"), 10, "Sushi rolls" }
                });

            migrationBuilder.InsertData(
                table: "FridgeProducts",
                columns: new[] { "FridgeProductId", "FridgeId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("30ed1b4b-b7f2-4fe5-9daa-608c8083ccff"), new Guid("2fcd1d29-5c3b-4089-b3d1-ec8524069741"), new Guid("20fadc8c-7b02-4668-b652-073bcde750fc"), 10 },
                    { new Guid("7a3192ae-1bcd-4a4a-85ed-c998acf2e2a7"), new Guid("2fcd1d29-5c3b-4089-b3d1-ec8524069741"), new Guid("1605d6a2-5e70-44ed-9393-bccb8e46b910"), 6 },
                    { new Guid("ffa74149-1ab8-42cf-9015-b4ffb95b0762"), new Guid("6092b048-afaf-429c-a95a-07f57ca3a58f"), new Guid("9b19425c-2503-48cb-b823-8a123b3a8ce3"), 19 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FridgeProducts",
                keyColumn: "FridgeProductId",
                keyValue: new Guid("30ed1b4b-b7f2-4fe5-9daa-608c8083ccff"));

            migrationBuilder.DeleteData(
                table: "FridgeProducts",
                keyColumn: "FridgeProductId",
                keyValue: new Guid("7a3192ae-1bcd-4a4a-85ed-c998acf2e2a7"));

            migrationBuilder.DeleteData(
                table: "FridgeProducts",
                keyColumn: "FridgeProductId",
                keyValue: new Guid("ffa74149-1ab8-42cf-9015-b4ffb95b0762"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("1605d6a2-5e70-44ed-9393-bccb8e46b910"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("20fadc8c-7b02-4668-b652-073bcde750fc"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("9b19425c-2503-48cb-b823-8a123b3a8ce3"));
        }
    }
}
