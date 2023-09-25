using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Re_ABP_Backend.Migrations
{
    /// <inheritdoc />
    public partial class NewNavigationOfManyToManyTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AudioBookAuthor_AudioBook_AudioBooksId",
                table: "AudioBookAuthor");

            migrationBuilder.RenameColumn(
                name: "AudioBooksId",
                table: "AudioBookAuthor",
                newName: "AudioBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_AudioBookAuthor_AudioBook_AudioBookId",
                table: "AudioBookAuthor",
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

            migrationBuilder.RenameColumn(
                name: "AudioBookId",
                table: "AudioBookAuthor",
                newName: "AudioBooksId");

            migrationBuilder.AddForeignKey(
                name: "FK_AudioBookAuthor_AudioBook_AudioBooksId",
                table: "AudioBookAuthor",
                column: "AudioBooksId",
                principalTable: "AudioBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
