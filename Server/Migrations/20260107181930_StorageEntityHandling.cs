using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yfitops.Server.Migrations
{
    /// <inheritdoc />
    public partial class StorageEntityHandling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Storages_StorageId",
                table: "Albums");

            migrationBuilder.RenameColumn(
                name: "StorageId",
                table: "Albums",
                newName: "CoverImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Albums_StorageId",
                table: "Albums",
                newName: "IX_Albums_CoverImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Storages_CoverImageId",
                table: "Albums",
                column: "CoverImageId",
                principalTable: "Storages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Storages_CoverImageId",
                table: "Albums");

            migrationBuilder.RenameColumn(
                name: "CoverImageId",
                table: "Albums",
                newName: "StorageId");

            migrationBuilder.RenameIndex(
                name: "IX_Albums_CoverImageId",
                table: "Albums",
                newName: "IX_Albums_StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Storages_StorageId",
                table: "Albums",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id");
        }
    }
}
