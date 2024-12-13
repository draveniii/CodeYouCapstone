using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCWebBanking.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Full_Database_Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Shares",
                keyColumn: "Id",
                keyValue: 10,
                column: "CurrentBalance",
                value: 100m);

            migrationBuilder.UpdateData(
                table: "Shares",
                keyColumn: "Id",
                keyValue: 11,
                column: "CurrentBalance",
                value: 4000m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Shares",
                keyColumn: "Id",
                keyValue: 10,
                column: "CurrentBalance",
                value: 2500m);

            migrationBuilder.UpdateData(
                table: "Shares",
                keyColumn: "Id",
                keyValue: 11,
                column: "CurrentBalance",
                value: 400m);
        }
    }
}
