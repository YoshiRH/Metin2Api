using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Metin2Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GuildFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GuildId",
                table: "Characters",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Guilds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GuildName = table.Column<string>(type: "text", nullable: false),
                    MasterId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guilds", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_GuildId",
                table: "Characters",
                column: "GuildId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Guilds_GuildId",
                table: "Characters",
                column: "GuildId",
                principalTable: "Guilds",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Guilds_GuildId",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "Guilds");

            migrationBuilder.DropIndex(
                name: "IX_Characters_GuildId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "GuildId",
                table: "Characters");
        }
    }
}
