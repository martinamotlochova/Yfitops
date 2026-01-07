using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yfitops.Server.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationsDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumApplicationUser_Albums_FavoriteAlbumsId",
                table: "AlbumApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_AlbumApplicationUser_AspNetUsers_UsersId",
                table: "AlbumApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserArtist_Artists_FavoriteArtistsId",
                table: "ApplicationUserArtist");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserArtist_AspNetUsers_UsersId",
                table: "ApplicationUserArtist");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserTrack_AspNetUsers_UsersId",
                table: "ApplicationUserTrack");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserTrack_Tracks_FavoriteTracksId",
                table: "ApplicationUserTrack");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "ApplicationUserTrack",
                newName: "UserFavoritesId");

            migrationBuilder.RenameColumn(
                name: "FavoriteTracksId",
                table: "ApplicationUserTrack",
                newName: "TrackFavouritesId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserTrack_UsersId",
                table: "ApplicationUserTrack",
                newName: "IX_ApplicationUserTrack_UserFavoritesId");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "ApplicationUserArtist",
                newName: "UserFavoritesId");

            migrationBuilder.RenameColumn(
                name: "FavoriteArtistsId",
                table: "ApplicationUserArtist",
                newName: "ArtistFavouritesId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserArtist_UsersId",
                table: "ApplicationUserArtist",
                newName: "IX_ApplicationUserArtist_UserFavoritesId");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "AlbumApplicationUser",
                newName: "UserFavoritesId");

            migrationBuilder.RenameColumn(
                name: "FavoriteAlbumsId",
                table: "AlbumApplicationUser",
                newName: "AlbumFavouritesId");

            migrationBuilder.RenameIndex(
                name: "IX_AlbumApplicationUser_UsersId",
                table: "AlbumApplicationUser",
                newName: "IX_AlbumApplicationUser_UserFavoritesId");

            migrationBuilder.AddColumn<Guid>(
                name: "StogareId",
                table: "Tracks",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StorageId",
                table: "Tracks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReleaseDate",
                table: "Albums",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "StorageId",
                table: "Albums",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    Data = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Size = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_StorageId",
                table: "Tracks",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_StorageId",
                table: "Albums",
                column: "StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumApplicationUser_Albums_AlbumFavouritesId",
                table: "AlbumApplicationUser",
                column: "AlbumFavouritesId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumApplicationUser_AspNetUsers_UserFavoritesId",
                table: "AlbumApplicationUser",
                column: "UserFavoritesId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Storages_StorageId",
                table: "Albums",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserArtist_Artists_ArtistFavouritesId",
                table: "ApplicationUserArtist",
                column: "ArtistFavouritesId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserArtist_AspNetUsers_UserFavoritesId",
                table: "ApplicationUserArtist",
                column: "UserFavoritesId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserTrack_AspNetUsers_UserFavoritesId",
                table: "ApplicationUserTrack",
                column: "UserFavoritesId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserTrack_Tracks_TrackFavouritesId",
                table: "ApplicationUserTrack",
                column: "TrackFavouritesId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Storages_StorageId",
                table: "Tracks",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumApplicationUser_Albums_AlbumFavouritesId",
                table: "AlbumApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_AlbumApplicationUser_AspNetUsers_UserFavoritesId",
                table: "AlbumApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Storages_StorageId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserArtist_Artists_ArtistFavouritesId",
                table: "ApplicationUserArtist");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserArtist_AspNetUsers_UserFavoritesId",
                table: "ApplicationUserArtist");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserTrack_AspNetUsers_UserFavoritesId",
                table: "ApplicationUserTrack");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserTrack_Tracks_TrackFavouritesId",
                table: "ApplicationUserTrack");

            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Storages_StorageId",
                table: "Tracks");

            migrationBuilder.DropTable(
                name: "Storages");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_StorageId",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Albums_StorageId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "StogareId",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "Albums");

            migrationBuilder.RenameColumn(
                name: "UserFavoritesId",
                table: "ApplicationUserTrack",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "TrackFavouritesId",
                table: "ApplicationUserTrack",
                newName: "FavoriteTracksId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserTrack_UserFavoritesId",
                table: "ApplicationUserTrack",
                newName: "IX_ApplicationUserTrack_UsersId");

            migrationBuilder.RenameColumn(
                name: "UserFavoritesId",
                table: "ApplicationUserArtist",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "ArtistFavouritesId",
                table: "ApplicationUserArtist",
                newName: "FavoriteArtistsId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserArtist_UserFavoritesId",
                table: "ApplicationUserArtist",
                newName: "IX_ApplicationUserArtist_UsersId");

            migrationBuilder.RenameColumn(
                name: "UserFavoritesId",
                table: "AlbumApplicationUser",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "AlbumFavouritesId",
                table: "AlbumApplicationUser",
                newName: "FavoriteAlbumsId");

            migrationBuilder.RenameIndex(
                name: "IX_AlbumApplicationUser_UserFavoritesId",
                table: "AlbumApplicationUser",
                newName: "IX_AlbumApplicationUser_UsersId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReleaseDate",
                table: "Albums",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumApplicationUser_Albums_FavoriteAlbumsId",
                table: "AlbumApplicationUser",
                column: "FavoriteAlbumsId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumApplicationUser_AspNetUsers_UsersId",
                table: "AlbumApplicationUser",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserArtist_Artists_FavoriteArtistsId",
                table: "ApplicationUserArtist",
                column: "FavoriteArtistsId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserArtist_AspNetUsers_UsersId",
                table: "ApplicationUserArtist",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserTrack_AspNetUsers_UsersId",
                table: "ApplicationUserTrack",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserTrack_Tracks_FavoriteTracksId",
                table: "ApplicationUserTrack",
                column: "FavoriteTracksId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
