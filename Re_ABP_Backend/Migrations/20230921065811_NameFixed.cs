using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Re_ABP_Backend.Migrations
{
    /// <inheritdoc />
    public partial class NameFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AudioBookAuthor_Genre_GenreId",
                table: "AudioBookAuthor");

            migrationBuilder.DropIndex(
                name: "IX_AudioBookAuthor_GenreId",
                table: "AudioBookAuthor");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "AudioBookAuthor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "AudioBookAuthor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AudioBookAuthor_GenreId",
                table: "AudioBookAuthor",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_AudioBookAuthor_Genre_GenreId",
                table: "AudioBookAuthor",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id");
        }
    }
}
