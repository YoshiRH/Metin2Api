using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Metin2Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttackPower",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DefensePower",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "RequiredLevel",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "requiredLevel",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "Items",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "AttackPower",
                table: "Items",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DefensePower",
                table: "Items",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemType",
                table: "Items",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RequiredLevel",
                table: "Items",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "requiredLevel",
                table: "Items",
                type: "integer",
                nullable: true);
        }
    }
}
