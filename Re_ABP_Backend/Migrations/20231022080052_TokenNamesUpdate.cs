using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Re_ABP_Backend.Migrations
{
    /// <inheritdoc />
    public partial class TokenNamesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Expires",
                table: "User",
                newName: "TokenExpires");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "User",
                newName: "TokenCreated");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TokenExpires",
                table: "User",
                newName: "Expires");

            migrationBuilder.RenameColumn(
                name: "TokenCreated",
                table: "User",
                newName: "Created");
        }
    }
}
