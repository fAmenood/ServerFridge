using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerFridge.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "FridgeProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "FridgeProducts");
        }
    }
}
