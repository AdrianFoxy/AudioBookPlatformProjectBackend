using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Re_ABP_Backend.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    EnDescription = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookAudioFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AudioFileUrl = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    PlaybackQueue = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAudioFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookLanguage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookLanguage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookSelection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    EnDescription = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookSelection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookSeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookSeries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LibraryStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Narrator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    MediaUrl = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Narrator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AudioBook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    PictureUrl = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    BookDuration = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ViewCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    BookLanguageId = table.Column<int>(type: "int", nullable: false),
                    NarratorId = table.Column<int>(type: "int", nullable: false),
                    BookSeriesId = table.Column<int>(type: "int", nullable: false),
                    OrderInSeries = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AudioBook_BookLanguage_BookLanguageId",
                        column: x => x.BookLanguageId,
                        principalTable: "BookLanguage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AudioBook_BookSeries_BookSeriesId",
                        column: x => x.BookSeriesId,
                        principalTable: "BookSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AudioBook_Narrator_NarratorId",
                        column: x => x.NarratorId,
                        principalTable: "Narrator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    About = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    SocialAuth = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(256)", maxLength: 256, nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(256)", maxLength: 256, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    TokenCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    TokenExpires = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "AudioBookAuthor",
                columns: table => new
                {
                    AudioBookId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioBookAuthor", x => new { x.AudioBookId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_AudioBookAuthor_AudioBook_AudioBookId",
                        column: x => x.AudioBookId,
                        principalTable: "AudioBook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AudioBookAuthor_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AudioBookGenre",
                columns: table => new
                {
                    AudioBookId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioBookGenre", x => new { x.AudioBookId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_AudioBookGenre_AudioBook_AudioBookId",
                        column: x => x.AudioBookId,
                        principalTable: "AudioBook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AudioBookGenre_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewText = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    AudioBookId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_AudioBook_AudioBookId",
                        column: x => x.AudioBookId,
                        principalTable: "AudioBook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Review_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLibrary",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AudioBookId = table.Column<int>(type: "int", nullable: false),
                    LibraryStatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLibrary", x => new { x.AudioBookId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserLibrary_AudioBook_AudioBookId",
                        column: x => x.AudioBookId,
                        principalTable: "AudioBook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLibrary_LibraryStatus_LibraryStatusId",
                        column: x => x.LibraryStatusId,
                        principalTable: "LibraryStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLibrary_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AudioBook_BookLanguageId",
                table: "AudioBook",
                column: "BookLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_AudioBook_BookSeriesId",
                table: "AudioBook",
                column: "BookSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_AudioBook_Name",
                table: "AudioBook",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AudioBook_NarratorId",
                table: "AudioBook",
                column: "NarratorId");

            migrationBuilder.CreateIndex(
                name: "IX_AudioBookAudioFile_BookAudioFileId",
                table: "AudioBookAudioFile",
                column: "BookAudioFileId");

            migrationBuilder.CreateIndex(
                name: "IX_AudioBookAuthor_AuthorId",
                table: "AudioBookAuthor",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_AudioBookGenre_GenreId",
                table: "AudioBookGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_AudioBookSelection_BookSelectionId",
                table: "AudioBookSelection",
                column: "BookSelectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Author_EnName",
                table: "Author",
                column: "EnName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Author_Name",
                table: "Author",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookSelection_EnName",
                table: "BookSelection",
                column: "EnName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookSelection_Name",
                table: "BookSelection",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookSeries_EnName",
                table: "BookSeries",
                column: "EnName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookSeries_Name",
                table: "BookSeries",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genre_EnName",
                table: "Genre",
                column: "EnName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genre_Name",
                table: "Genre",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Narrator_Name",
                table: "Narrator",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_AudioBookId",
                table: "Review",
                column: "AudioBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                table: "Review",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLibrary_LibraryStatusId",
                table: "UserLibrary",
                column: "LibraryStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLibrary_UserId",
                table: "UserLibrary",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AudioBookAudioFile");

            migrationBuilder.DropTable(
                name: "AudioBookAuthor");

            migrationBuilder.DropTable(
                name: "AudioBookGenre");

            migrationBuilder.DropTable(
                name: "AudioBookSelection");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "UserLibrary");

            migrationBuilder.DropTable(
                name: "BookAudioFile");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "BookSelection");

            migrationBuilder.DropTable(
                name: "AudioBook");

            migrationBuilder.DropTable(
                name: "LibraryStatus");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "BookLanguage");

            migrationBuilder.DropTable(
                name: "BookSeries");

            migrationBuilder.DropTable(
                name: "Narrator");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
