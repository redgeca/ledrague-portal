using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ledrague_portal.Data.Migrations
{
    public partial class CategorySongM2MRL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KaraokeCategorySongs",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SongId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaraokeCategorySongs", x => new { x.CategoryId, x.SongId });
                    table.ForeignKey(
                        name: "FK_KaraokeCategorySongs_KaraokeCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "KaraokeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KaraokeCategorySongs_KaraokeSongs_SongId",
                        column: x => x.SongId,
                        principalTable: "KaraokeSongs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KaraokeCategorySongs_SongId",
                table: "KaraokeCategorySongs",
                column: "SongId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KaraokeCategorySongs");
        }
    }
}
