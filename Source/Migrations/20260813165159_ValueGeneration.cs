using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookmarks.Migrations
{
    /// <inheritdoc />
    public partial class ValueGeneration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookmarks_Name_Url",
                table: "Bookmarks");

            migrationBuilder.DropIndex(
                name: "IX_Bookmarks_OwnerId",
                table: "Bookmarks");

            migrationBuilder.DropIndex(
                name: "IX_BookmarkGroups_Name",
                table: "BookmarkGroups");

            migrationBuilder.DropIndex(
                name: "IX_BookmarkGroups_OwnerId",
                table: "BookmarkGroups");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BookmarkGroups",
                type: "TEXT",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 500);

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_OwnerId_Name_Url",
                table: "Bookmarks",
                columns: new[] { "OwnerId", "Name", "Url" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookmarkGroups_OwnerId_Name",
                table: "BookmarkGroups",
                columns: new[] { "OwnerId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookmarks_OwnerId_Name_Url",
                table: "Bookmarks");

            migrationBuilder.DropIndex(
                name: "IX_BookmarkGroups_OwnerId_Name",
                table: "BookmarkGroups");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BookmarkGroups",
                type: "TEXT",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_Name_Url",
                table: "Bookmarks",
                columns: new[] { "Name", "Url" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_OwnerId",
                table: "Bookmarks",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_BookmarkGroups_Name",
                table: "BookmarkGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookmarkGroups_OwnerId",
                table: "BookmarkGroups",
                column: "OwnerId");
        }
    }
}
