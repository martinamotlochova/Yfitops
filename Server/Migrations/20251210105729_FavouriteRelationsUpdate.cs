using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yfitops.Server.Migrations
{
    /// <inheritdoc />
    public partial class FavouriteRelationsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favourite_Albums_AlbumId",
                table: "Favourite");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourite_Artists_ArtistId",
                table: "Favourite");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourite_AspNetUsers_UserId1",
                table: "Favourite");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourite_Tracks_TrackId",
                table: "Favourite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favourite",
                table: "Favourite");

            migrationBuilder.RenameTable(
                name: "Favourite",
                newName: "Favourites");

            migrationBuilder.RenameIndex(
                name: "IX_Favourite_UserId1",
                table: "Favourites",
                newName: "IX_Favourites_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Favourite_TrackId",
                table: "Favourites",
                newName: "IX_Favourites_TrackId");

            migrationBuilder.RenameIndex(
                name: "IX_Favourite_ArtistId",
                table: "Favourites",
                newName: "IX_Favourites_ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_Favourite_AlbumId",
                table: "Favourites",
                newName: "IX_Favourites_AlbumId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favourites",
                table: "Favourites",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Albums_AlbumId",
                table: "Favourites",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Artists_ArtistId",
                table: "Favourites",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_AspNetUsers_UserId1",
                table: "Favourites",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Tracks_TrackId",
                table: "Favourites",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Albums_AlbumId",
                table: "Favourites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Artists_ArtistId",
                table: "Favourites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_AspNetUsers_UserId1",
                table: "Favourites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Tracks_TrackId",
                table: "Favourites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favourites",
                table: "Favourites");

            migrationBuilder.RenameTable(
                name: "Favourites",
                newName: "Favourite");

            migrationBuilder.RenameIndex(
                name: "IX_Favourites_UserId1",
                table: "Favourite",
                newName: "IX_Favourite_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Favourites_TrackId",
                table: "Favourite",
                newName: "IX_Favourite_TrackId");

            migrationBuilder.RenameIndex(
                name: "IX_Favourites_ArtistId",
                table: "Favourite",
                newName: "IX_Favourite_ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_Favourites_AlbumId",
                table: "Favourite",
                newName: "IX_Favourite_AlbumId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favourite",
                table: "Favourite",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourite_Albums_AlbumId",
                table: "Favourite",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favourite_Artists_ArtistId",
                table: "Favourite",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favourite_AspNetUsers_UserId1",
                table: "Favourite",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourite_Tracks_TrackId",
                table: "Favourite",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
