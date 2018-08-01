using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ledrague_portal.Data.Migrations
{
    public partial class remove_category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KaraokeSongs_KaraokeCategories_CategoryId",
                table: "KaraokeSongs");

            migrationBuilder.DropIndex(
                name: "IX_KaraokeSongs_CategoryId",
                table: "KaraokeSongs");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "KaraokeSongs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "KaraokeSongs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_KaraokeSongs_CategoryId",
                table: "KaraokeSongs",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_KaraokeSongs_KaraokeCategories_CategoryId",
                table: "KaraokeSongs",
                column: "CategoryId",
                principalTable: "KaraokeCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
