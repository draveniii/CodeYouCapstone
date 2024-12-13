using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVCWebBanking.Migrations
{
    /// <inheritdoc />
    public partial class Added_Partial_Share_Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Shares",
                columns: new[] { "Id", "AccountId", "CurrentBalance", "InterestRate", "MinimumBalance", "Type" },
                values: new object[,]
                {
                    { 1, 1, 500m, 0.005m, 25m, "Savings" },
                    { 2, 1, 100m, 0m, 0m, "Checking" },
                    { 3, 1, 2000m, 0.1m, 1000m, "Money Market" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Shares",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shares",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Shares",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
