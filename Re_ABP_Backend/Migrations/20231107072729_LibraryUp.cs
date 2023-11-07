using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Re_ABP_Backend.Migrations
{
    /// <inheritdoc />
    public partial class LibraryUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UserLibrary",
                type: "DateTime",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "UserLibrary",
                type: "DateTime",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LibraryStatus",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "EnName",
                table: "LibraryStatus",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UserLibrary");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "UserLibrary");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "LibraryStatus",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<int>(
                name: "EnName",
                table: "LibraryStatus",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);
        }
    }
}
