using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABP_Backend.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyRework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AudioBookGenre_AudioBook_AudioBooksId",
                table: "AudioBookGenre");

            migrationBuilder.DropTable(
                name: "AudioBookBookAudioFile");

            migrationBuilder.DropTable(
                name: "AudioBookBookSelection");

            migrationBuilder.RenameColumn(
                name: "AudioBooksId",
                table: "AudioBookGenre",
                newName: "AudioBookId");

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "AudioBookAuthor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AudioBookAudioFile",
                columns: table => new
                {
                    AudioBookId = table.Column<int>(type: "int", nullable: false),
                    BookAudioFileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioBookAudioFile", x => new { x.AudioBookId, x.BookAudioFileId });
                    table.ForeignKey(
                        name: "FK_AudioBookAudioFile_AudioBook_AudioBookId",
                        column: x => x.AudioBookId,
                        principalTable: "AudioBook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AudioBookAudioFile_BookAudioFile_BookAudioFileId",
                        column: x => x.BookAudioFileId,
                        principalTable: "BookAudioFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AudioBookSelection",
                columns: table => new
                {
                    AudioBookId = table.Column<int>(type: "int", nullable: false),
                    BookSelectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioBookSelection", x => new { x.AudioBookId, x.BookSelectionId });
                    table.ForeignKey(
                        name: "FK_AudioBookSelection_AudioBook_AudioBookId",
                        column: x => x.AudioBookId,
                        principalTable: "AudioBook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AudioBookSelection_BookSelection_BookSelectionId",
                        column: x => x.BookSelectionId,
                        principalTable: "BookSelection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AudioBookAuthor_GenreId",
                table: "AudioBookAuthor",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_AudioBookAudioFile_BookAudioFileId",
                table: "AudioBookAudioFile",
                column: "BookAudioFileId");

            migrationBuilder.CreateIndex(
                name: "IX_AudioBookSelection_BookSelectionId",
                table: "AudioBookSelection",
                column: "BookSelectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AudioBookAuthor_Genre_GenreId",
                table: "AudioBookAuthor",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id");

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
                name: "FK_AudioBookAuthor_Genre_GenreId",
                table: "AudioBookAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_AudioBookGenre_AudioBook_AudioBookId",
                table: "AudioBookGenre");

            migrationBuilder.DropTable(
                name: "AudioBookAudioFile");

            migrationBuilder.DropTable(
                name: "AudioBookSelection");

            migrationBuilder.DropIndex(
                name: "IX_AudioBookAuthor_GenreId",
                table: "AudioBookAuthor");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "AudioBookAuthor");

            migrationBuilder.RenameColumn(
                name: "AudioBookId",
                table: "AudioBookGenre",
                newName: "AudioBooksId");

            migrationBuilder.CreateTable(
                name: "AudioBookBookAudioFile",
                columns: table => new
                {
                    AudioBooksId = table.Column<int>(type: "int", nullable: false),
                    BookAudioFileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioBookBookAudioFile", x => new { x.AudioBooksId, x.BookAudioFileId });
                    table.ForeignKey(
                        name: "FK_AudioBookBookAudioFile_AudioBook_AudioBooksId",
                        column: x => x.AudioBooksId,
                        principalTable: "AudioBook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AudioBookBookAudioFile_BookAudioFile_BookAudioFileId",
                        column: x => x.BookAudioFileId,
                        principalTable: "BookAudioFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AudioBookBookSelection",
                columns: table => new
                {
                    AudioBooksId = table.Column<int>(type: "int", nullable: false),
                    BookSelectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioBookBookSelection", x => new { x.AudioBooksId, x.BookSelectionId });
                    table.ForeignKey(
                        name: "FK_AudioBookBookSelection_AudioBook_AudioBooksId",
                        column: x => x.AudioBooksId,
                        principalTable: "AudioBook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AudioBookBookSelection_BookSelection_BookSelectionId",
                        column: x => x.BookSelectionId,
                        principalTable: "BookSelection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AudioBookBookAudioFile_BookAudioFileId",
                table: "AudioBookBookAudioFile",
                column: "BookAudioFileId");

            migrationBuilder.CreateIndex(
                name: "IX_AudioBookBookSelection_BookSelectionId",
                table: "AudioBookBookSelection",
                column: "BookSelectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AudioBookGenre_AudioBook_AudioBooksId",
                table: "AudioBookGenre",
                column: "AudioBooksId",
                principalTable: "AudioBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
