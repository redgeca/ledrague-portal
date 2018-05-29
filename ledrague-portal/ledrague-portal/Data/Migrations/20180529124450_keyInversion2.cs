using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ledrague_portal.Data.Migrations
{
    public partial class keyInversion2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KaraokeCategorySongs_KaraokeSongs_CategoryId",
                table: "KaraokeCategorySongs");

            migrationBuilder.DropForeignKey(
                name: "FK_KaraokeCategorySongs_KaraokeCategories_SongId",
                table: "KaraokeCategorySongs");

            migrationBuilder.AddForeignKey(
                name: "FK_KaraokeCategorySongs_KaraokeCategories_CategoryId",
                table: "KaraokeCategorySongs",
                column: "CategoryId",
                principalTable: "KaraokeCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KaraokeCategorySongs_KaraokeSongs_SongId",
                table: "KaraokeCategorySongs",
                column: "SongId",
                principalTable: "KaraokeSongs",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KaraokeCategorySongs_KaraokeCategories_CategoryId",
                table: "KaraokeCategorySongs");

            migrationBuilder.DropForeignKey(
                name: "FK_KaraokeCategorySongs_KaraokeSongs_SongId",
                table: "KaraokeCategorySongs");

            migrationBuilder.AddForeignKey(
                name: "FK_KaraokeCategorySongs_KaraokeSongs_CategoryId",
                table: "KaraokeCategorySongs",
                column: "CategoryId",
                principalTable: "KaraokeSongs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KaraokeCategorySongs_KaraokeCategories_SongId",
                table: "KaraokeCategorySongs",
                column: "SongId",
                principalTable: "KaraokeCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
