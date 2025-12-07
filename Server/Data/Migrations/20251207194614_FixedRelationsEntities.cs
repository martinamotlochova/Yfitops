using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yfitops.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedRelationsEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Album_AspNetUsers_UserId",
                table: "Album");

            migrationBuilder.DropForeignKey(
                name: "FK_Artists_AspNetUsers_UserId",
                table: "Artists");

            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_AspNetUsers_UserId",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_UserId",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Artists_UserId",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Album_UserId",
                table: "Album");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Album");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tracks",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Artists",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Album",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

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
                        name: "FK_AlbumApplicationUser_Album_FavoriteAlbumsId",
                        column: x => x.FavoriteAlbumsId,
                        principalTable: "Album",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumApplicationUser");

            migrationBuilder.DropTable(
                name: "ApplicationUserArtist");

            migrationBuilder.DropTable(
                name: "ApplicationUserTrack");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tracks",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Tracks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Artists",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Artists",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Album",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Album",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_UserId",
                table: "Tracks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_UserId",
                table: "Artists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Album_UserId",
                table: "Album",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Album_AspNetUsers_UserId",
                table: "Album",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_AspNetUsers_UserId",
                table: "Artists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_AspNetUsers_UserId",
                table: "Tracks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
