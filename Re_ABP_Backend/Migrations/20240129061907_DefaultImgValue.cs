using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Re_ABP_Backend.Migrations
{
    /// <inheritdoc />
    public partial class DefaultImgValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "BookSelection",
                type: "text",
                nullable: false,
                defaultValue: "/img/default_img.jpg",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Author",
                type: "text",
                nullable: false,
                defaultValue: "/img/default_img.jpg",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PictureUrl",
                table: "AudioBook",
                type: "text",
                nullable: false,
                defaultValue: "/img/default_img.jpg",
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "BookSelection",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "/img/default_img.jpg");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Author",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "/img/default_img.jpg");

            migrationBuilder.AlterColumn<string>(
                name: "PictureUrl",
                table: "AudioBook",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "/img/default_img.jpg");
        }
    }
}
