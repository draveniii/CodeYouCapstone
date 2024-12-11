using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCWebBanking.Migrations
{
    /// <inheritdoc />
    public partial class ToShare_ToAccount_Added_to_Transactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ToAccountId",
                table: "Transactions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToShareId",
                table: "Transactions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShareNumber",
                table: "Shares",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToAccountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ToShareId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ShareNumber",
                table: "Shares");
        }
    }
}
