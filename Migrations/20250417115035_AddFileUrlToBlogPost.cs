using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RadioAmateurHelper.Migrations
{
    /// <inheritdoc />
    public partial class AddFileUrlToBlogPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Circuits",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "BlogPosts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "BlogPosts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "BlogPosts",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "BlogPosts");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Circuits",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
