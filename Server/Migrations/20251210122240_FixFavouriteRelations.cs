using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yfitops.Server.Migrations
{
    /// <inheritdoc />
    public partial class FixFavouriteRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.CreateTable(
                name: "AlbumApplicationUser",
                columns: table => new
                {
                    AlbumFavouritesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserFavoritesId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumApplicationUser", x => new { x.AlbumFavouritesId, x.UserFavoritesId });
                    table.ForeignKey(
                        name: "FK_AlbumApplicationUser_Albums_AlbumFavouritesId",
                        column: x => x.AlbumFavouritesId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumApplicationUser_AspNetUsers_UserFavoritesId",
                        column: x => x.UserFavoritesId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserArtist",
                columns: table => new
                {
                    ArtistFavouritesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserFavoritesId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserArtist", x => new { x.ArtistFavouritesId, x.UserFavoritesId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserArtist_Artists_ArtistFavouritesId",
                        column: x => x.ArtistFavouritesId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserArtist_AspNetUsers_UserFavoritesId",
                        column: x => x.UserFavoritesId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserTrack",
                columns: table => new
                {
                    TrackFavouritesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserFavoritesId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserTrack", x => new { x.TrackFavouritesId, x.UserFavoritesId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserTrack_AspNetUsers_UserFavoritesId",
                        column: x => x.UserFavoritesId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserTrack_Tracks_TrackFavouritesId",
                        column: x => x.TrackFavouritesId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumApplicationUser_UserFavoritesId",
                table: "AlbumApplicationUser",
                column: "UserFavoritesId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserArtist_UserFavoritesId",
                table: "ApplicationUserArtist",
                column: "UserFavoritesId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserTrack_UserFavoritesId",
                table: "ApplicationUserTrack",
                column: "UserFavoritesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumApplicationUser");

            migrationBuilder.DropTable(
                name: "ApplicationUserArtist");

            migrationBuilder.DropTable(
                name: "ApplicationUserTrack");

            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AlbumId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ArtistId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TrackId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId1 = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favourites_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favourites_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favourites_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Favourites_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_AlbumId",
                table: "Favourites",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_ArtistId",
                table: "Favourites",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_TrackId",
                table: "Favourites",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_UserId1",
                table: "Favourites",
                column: "UserId1");
        }
    }
}
