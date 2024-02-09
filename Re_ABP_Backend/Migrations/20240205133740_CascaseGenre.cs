using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Re_ABP_Backend.Migrations
{
    /// <inheritdoc />
    public partial class CascaseGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AudioBookAuthor_AudioBook_AudioBookId",
                table: "AudioBookAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_AudioBookGenre_AudioBook_AudioBookId",
                table: "AudioBookGenre");

            migrationBuilder.AddForeignKey(
                name: "FK_AudioBookAuthor_AudioBook_AudioBookId",
                table: "AudioBookAuthor",
                column: "AudioBookId",
                principalTable: "AudioBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AudioBookGenre_AudioBook_AudioBookId",
                table: "AudioBookGenre",
                column: "AudioBookId",
                principalTable: "AudioBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AudioBookAuthor_AudioBook_AudioBookId",
                table: "AudioBookAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_AudioBookGenre_AudioBook_AudioBookId",
                table: "AudioBookGenre");

            migrationBuilder.AddForeignKey(
                name: "FK_AudioBookAuthor_AudioBook_AudioBookId",
                table: "AudioBookAuthor",
                column: "AudioBookId",
                principalTable: "AudioBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AudioBookGenre_AudioBook_AudioBookId",
                table: "AudioBookGenre",
                column: "AudioBookId",
                principalTable: "AudioBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
