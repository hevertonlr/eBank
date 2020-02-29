using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace eBank.Infra.Data.Migrations
{
    public partial class InitDatabaseMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Number = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Balance = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Number);
                });

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "Number", "Balance" },
                values: new object[] { 54321, 160 }
            );
            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "Number", "Balance" },
                values: new object[] { 123, 99999m }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccounts");
        }
    }
}
