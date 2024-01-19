using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Re_ABP_Backend.Migrations
{
    /// <inheritdoc />
    public partial class UniqLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BookLanguage_EnName",
                table: "BookLanguage",
                column: "EnName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookLanguage_Name",
                table: "BookLanguage",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookLanguage_EnName",
                table: "BookLanguage");

            migrationBuilder.DropIndex(
                name: "IX_BookLanguage_Name",
                table: "BookLanguage");
        }
    }
}
