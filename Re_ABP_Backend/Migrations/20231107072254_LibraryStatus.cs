using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Re_ABP_Backend.Migrations
{
    /// <inheritdoc />
    public partial class LibraryStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LibraryStatusId",
                table: "UserLibrary",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LibraryStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false),
                    EnName = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLibrary_LibraryStatusId",
                table: "UserLibrary",
                column: "LibraryStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLibrary_LibraryStatus_LibraryStatusId",
                table: "UserLibrary",
                column: "LibraryStatusId",
                principalTable: "LibraryStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLibrary_LibraryStatus_LibraryStatusId",
                table: "UserLibrary");

            migrationBuilder.DropTable(
                name: "LibraryStatus");

            migrationBuilder.DropIndex(
                name: "IX_UserLibrary_LibraryStatusId",
                table: "UserLibrary");

            migrationBuilder.DropColumn(
                name: "LibraryStatusId",
                table: "UserLibrary");
        }
    }
}
