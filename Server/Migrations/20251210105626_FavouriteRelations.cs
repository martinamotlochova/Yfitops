using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yfitops.Server.Migrations
{
    /// <inheritdoc />
    public partial class FavouriteRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumApplicationUser");

            migrationBuilder.DropTable(
                name: "ApplicationUserArtist");

            migrationBuilder.DropTable(
                name: "ApplicationUserTrack");

            migrationBuilder.CreateTable(
                name: "Favourite",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ArtistId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AlbumId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TrackId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favourite_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favourite_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favourite_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Favourite_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favourite_AlbumId",
                table: "Favourite",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Favourite_ArtistId",
                table: "Favourite",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Favourite_TrackId",
                table: "Favourite",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Favourite_UserId1",
                table: "Favourite",
                column: "UserId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favourite");

            migrationBuilder.CreateTable(
                name: "AlbumApplicationUser",
                columns: table => new
                {
                    FavoriteAlbumsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsersId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumApplicationUser", x => new { x.FavoriteAlbumsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_AlbumApplicationUser_Albums_FavoriteAlbumsId",
                        column: x => x.FavoriteAlbumsId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumApplicationUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserArtist",
                columns: table => new
                {
                    FavoriteArtistsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsersId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserArtist", x => new { x.FavoriteArtistsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserArtist_Artists_FavoriteArtistsId",
                        column: x => x.FavoriteArtistsId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserArtist_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserTrack",
                columns: table => new
                {
                    FavoriteTracksId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsersId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserTrack", x => new { x.FavoriteTracksId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserTrack_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserTrack_Tracks_FavoriteTracksId",
                        column: x => x.FavoriteTracksId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumApplicationUser_UsersId",
                table: "AlbumApplicationUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserArtist_UsersId",
                table: "ApplicationUserArtist",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserTrack_UsersId",
                table: "ApplicationUserTrack",
                column: "UsersId");
        }
    }
}
