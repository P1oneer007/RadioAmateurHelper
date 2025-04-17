using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RadioAmateurHelper.Migrations
{
    /// <inheritdoc />
    public partial class AddComponentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ComponentModels",
                table: "ComponentModels");

            migrationBuilder.RenameTable(
                name: "ComponentModels",
                newName: "Components");

            migrationBuilder.AddColumn<string>(
                name: "Characteristics",
                table: "Components",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Components",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Components",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "SchematicSymbolUrl",
                table: "Components",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Components",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Components",
                table: "Components",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Components",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Characteristics",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "SchematicSymbolUrl",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Components");

            migrationBuilder.RenameTable(
                name: "Components",
                newName: "ComponentModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComponentModels",
                table: "ComponentModels",
                column: "Id");
        }
    }
}
