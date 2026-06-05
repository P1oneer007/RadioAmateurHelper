using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RadioAmateurHelper.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminSpreadsheetAndExchangePrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ExchangeEntries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdminSpreadsheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataJson = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminSpreadsheets", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminSpreadsheets");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ExchangeEntries");
        }
    }
}
