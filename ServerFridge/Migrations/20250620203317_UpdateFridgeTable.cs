using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServerFridge.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFridgeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fridges_FridgeModels_ModelId",
                table: "Fridges");

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

            migrationBuilder.RenameColumn(
                name: "ModelId",
                table: "Fridges",
                newName: "Model Id");

            migrationBuilder.RenameIndex(
                name: "IX_Fridges_ModelId",
                table: "Fridges",
                newName: "IX_Fridges_Model Id");

            migrationBuilder.InsertData(
                table: "Fridges",
                columns: new[] { "Fridge Id", "Model Id", "Name", "OwnerName" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"), new Guid("3b93b477-08a5-4e4d-8fb7-637c47adbea1"), "Appolon", "Ivan Ivanov" },
                    { new Guid("b2c3d4e5-2345-6789-0123-bcdef1234567"), new Guid("aaaea151-8e40-4276-8a6a-1c275b120c1f"), "Oxygen 3.0", "Alex Alexdrov" },
                    { new Guid("d4e5f6f7-4567-8901-2345-def123456789"), new Guid("3b93b477-08a5-4e4d-8fb7-637c47adbea1"), "Nevermore", "Nikolai Nikolaev" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Fridges_FridgeModels_Model Id",
                table: "Fridges",
                column: "Model Id",
                principalTable: "FridgeModels",
                principalColumn: "ModelId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fridges_FridgeModels_Model Id",
                table: "Fridges");

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "Fridge Id",
                keyValue: new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "Fridge Id",
                keyValue: new Guid("b2c3d4e5-2345-6789-0123-bcdef1234567"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "Fridge Id",
                keyValue: new Guid("d4e5f6f7-4567-8901-2345-def123456789"));

            migrationBuilder.RenameColumn(
                name: "Model Id",
                table: "Fridges",
                newName: "ModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Fridges_Model Id",
                table: "Fridges",
                newName: "IX_Fridges_ModelId");

            migrationBuilder.InsertData(
                table: "Fridges",
                columns: new[] { "Fridge Id", "ModelId", "Name", "OwnerName" },
                values: new object[,]
                {
                    { new Guid("2f46b686-1436-46db-a85c-9863bcadd7ea"), new Guid("3b93b477-08a5-4e4d-8fb7-637c47adbea1"), "Nevermore", "Nikolai Nikolaev" },
                    { new Guid("2fcd1d29-5c3b-4089-b3d1-ec8524069741"), new Guid("3b93b477-08a5-4e4d-8fb7-637c47adbea1"), "Appolon", "Ivan Ivanov" },
                    { new Guid("6092b048-afaf-429c-a95a-07f57ca3a58f"), new Guid("aaaea151-8e40-4276-8a6a-1c275b120c1f"), "Oxygen 3.0", "Alex Alexdrov" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Fridges_FridgeModels_ModelId",
                table: "Fridges",
                column: "ModelId",
                principalTable: "FridgeModels",
                principalColumn: "ModelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
