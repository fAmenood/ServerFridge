using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServerFridge.Migrations
{
    /// <inheritdoc />
    public partial class AddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FridgeModels",
                columns: new[] { "ModelId", "Name", "Year" },
                values: new object[,]
                {
                    { new Guid("3b93b477-08a5-4e4d-8fb7-637c47adbea1"), "Lenovo", 2014 },
                    { new Guid("aaaea151-8e40-4276-8a6a-1c275b120c1f"), "Xiaomi", 2013 }
                });

            migrationBuilder.InsertData(
                table: "Fridges",
                columns: new[] { "Fridge Id", "ModelId", "Name", "OwnerName" },
                values: new object[,]
                {
                    { new Guid("2f46b686-1436-46db-a85c-9863bcadd7ea"), new Guid("3b93b477-08a5-4e4d-8fb7-637c47adbea1"), "Nevermore", "Nikolai Nikolaev" },
                    { new Guid("2fcd1d29-5c3b-4089-b3d1-ec8524069741"), new Guid("3b93b477-08a5-4e4d-8fb7-637c47adbea1"), "Appolon", "Ivan Ivanov" },
                    { new Guid("6092b048-afaf-429c-a95a-07f57ca3a58f"), new Guid("aaaea151-8e40-4276-8a6a-1c275b120c1f"), "Oxygen 3.0", "Alex Alexdrov" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "Fridge Id",
                keyValue: new Guid("2f46b686-1436-46db-a85c-9863bcadd7ea"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "Fridge Id",
                keyValue: new Guid("2fcd1d29-5c3b-4089-b3d1-ec8524069741"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "Fridge Id",
                keyValue: new Guid("6092b048-afaf-429c-a95a-07f57ca3a58f"));

            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "ModelId",
                keyValue: new Guid("3b93b477-08a5-4e4d-8fb7-637c47adbea1"));

            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "ModelId",
                keyValue: new Guid("aaaea151-8e40-4276-8a6a-1c275b120c1f"));
        }
    }
}
