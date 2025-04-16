using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RadioAmateurHelper.Migrations
{
    /// <inheritdoc />
    public partial class AddCircuitCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Circuits",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Circuits");
        }
    }
}
