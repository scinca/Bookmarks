using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookmarks.Migrations
{
    /// <inheritdoc />
    public partial class RenameGroupIDTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_BookmarkGroups_GroudId",
                table: "Bookmarks");

            migrationBuilder.RenameColumn(
                name: "GroudId",
                table: "Bookmarks",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmarks_GroudId",
                table: "Bookmarks",
                newName: "IX_Bookmarks_GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_BookmarkGroups_GroupId",
                table: "Bookmarks",
                column: "GroupId",
                principalTable: "BookmarkGroups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_BookmarkGroups_GroupId",
                table: "Bookmarks");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Bookmarks",
                newName: "GroudId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmarks_GroupId",
                table: "Bookmarks",
                newName: "IX_Bookmarks_GroudId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_BookmarkGroups_GroudId",
                table: "Bookmarks",
                column: "GroudId",
                principalTable: "BookmarkGroups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
