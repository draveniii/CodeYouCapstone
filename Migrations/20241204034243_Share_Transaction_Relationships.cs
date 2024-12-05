using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCWebBanking.Migrations
{
    /// <inheritdoc />
    public partial class Share_Transaction_Relationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shares_Accounts_AccountId",
                table: "Shares");

            migrationBuilder.AddColumn<int>(
                name: "ShareId",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Shares",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ShareId",
                table: "Transactions",
                column: "ShareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shares_Accounts_AccountId",
                table: "Shares",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Shares_ShareId",
                table: "Transactions",
                column: "ShareId",
                principalTable: "Shares",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shares_Accounts_AccountId",
                table: "Shares");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Shares_ShareId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ShareId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ShareId",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Shares",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Shares_Accounts_AccountId",
                table: "Shares",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
