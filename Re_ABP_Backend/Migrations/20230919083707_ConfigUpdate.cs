using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Re_ABP_Backend.Migrations
{
    /// <inheritdoc />
    public partial class ConfigUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Author_AudioBook_AudioBookId",
                table: "Author");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAudioFile_AudioBook_AudioBookId",
                table: "BookAudioFile");

            migrationBuilder.DropForeignKey(
                name: "FK_BookSelection_AudioBook_AudioBookId",
                table: "BookSelection");

            migrationBuilder.DropIndex(
                name: "IX_BookSelection_AudioBookId",
                table: "BookSelection");

            migrationBuilder.DropIndex(
                name: "IX_BookAudioFile_AudioBookId",
                table: "BookAudioFile");

            migrationBuilder.DropIndex(
                name: "IX_Author_AudioBookId",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "AudioBookId",
                table: "BookSelection");

            migrationBuilder.DropColumn(
                name: "AudioBookId",
                table: "BookAudioFile");

            migrationBuilder.DropColumn(
                name: "AudioBookId",
                table: "Author");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Narrator",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MediaUrl",
                table: "Narrator",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BookSeries",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BookSelection",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "BookSelection",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BookSelection",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BookLanguage",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BookAudioFile",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Duration",
                table: "BookAudioFile",
                type: "time",
                nullable: false,
                defaultValueSql: "'00:00:00'",
                oldClrType: typeof(TimeSpan),
                oldType: "TIME");

            migrationBuilder.AlterColumn<string>(
                name: "AudioFileUrl",
                table: "BookAudioFile",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Author",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Author",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Author",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AudioBookAuthor",
                columns: table => new
                {
                    AudioBooksId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioBookAuthor", x => new { x.AudioBooksId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_AudioBookAuthor_AudioBook_AudioBooksId",
                        column: x => x.AudioBooksId,
                        principalTable: "AudioBook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AudioBookAuthor_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_AudioBookAuthor_AuthorId",
                table: "AudioBookAuthor",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_AudioBookBookAudioFile_BookAudioFileId",
                table: "AudioBookBookAudioFile",
                column: "BookAudioFileId");

            migrationBuilder.CreateIndex(
                name: "IX_AudioBookBookSelection_BookSelectionId",
                table: "AudioBookBookSelection",
                column: "BookSelectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AudioBookAuthor");

            migrationBuilder.DropTable(
                name: "AudioBookBookAudioFile");

            migrationBuilder.DropTable(
                name: "AudioBookBookSelection");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Narrator",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "MediaUrl",
                table: "Narrator",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BookSeries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BookSelection",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "BookSelection",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BookSelection",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "AudioBookId",
                table: "BookSelection",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BookLanguage",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BookAudioFile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Duration",
                table: "BookAudioFile",
                type: "TIME",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldDefaultValueSql: "'00:00:00'");

            migrationBuilder.AlterColumn<string>(
                name: "AudioFileUrl",
                table: "BookAudioFile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "AudioBookId",
                table: "BookAudioFile",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Author",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Author",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Author",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "AudioBookId",
                table: "Author",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookSelection_AudioBookId",
                table: "BookSelection",
                column: "AudioBookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookAudioFile_AudioBookId",
                table: "BookAudioFile",
                column: "AudioBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Author_AudioBookId",
                table: "Author",
                column: "AudioBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Author_AudioBook_AudioBookId",
                table: "Author",
                column: "AudioBookId",
                principalTable: "AudioBook",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAudioFile_AudioBook_AudioBookId",
                table: "BookAudioFile",
                column: "AudioBookId",
                principalTable: "AudioBook",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookSelection_AudioBook_AudioBookId",
                table: "BookSelection",
                column: "AudioBookId",
                principalTable: "AudioBook",
                principalColumn: "Id");
        }
    }
}
