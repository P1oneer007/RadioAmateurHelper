using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RadioAmateurHelper.Migrations
{
    /// <inheritdoc />
    public partial class AddPostOwnershipAndSiteSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "BlogPosts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorUserId",
                table: "BlogPosts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SiteSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AllowUsersToDeleteOwnPosts = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSettings", x => x.Id);
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "AuthorUserId",
                table: "BlogPosts");
        }
    }
}
