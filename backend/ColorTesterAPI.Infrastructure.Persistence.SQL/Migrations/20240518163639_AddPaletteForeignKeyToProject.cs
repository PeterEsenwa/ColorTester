using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColorTesterAPI.Infrastructure.Persistence.SQL.Migrations
{
    /// <inheritdoc />
    public partial class AddPaletteForeignKeyToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_palettes_Name",
                table: "palettes");

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    palette_id = table.Column<Guid>(type: "uuid", nullable: true),
                    mockup_name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.id);
                    table.ForeignKey(
                        name: "FK_projects_palettes_palette_id",
                        column: x => x.palette_id,
                        principalTable: "palettes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_palettes_Name",
                table: "palettes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_projects_palette_id",
                table: "projects",
                column: "palette_id");

            migrationBuilder.CreateIndex(
                name: "IX_projects_user_id",
                table: "projects",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropIndex(
                name: "IX_palettes_Name",
                table: "palettes");

            migrationBuilder.CreateIndex(
                name: "IX_palettes_Name",
                table: "palettes",
                column: "Name");
        }
    }
}
