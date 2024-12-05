using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCWebBanking.Migrations
{
    /// <inheritdoc />
    public partial class Add_Share_Current_Balance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentBalance",
                table: "Shares",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentBalance",
                table: "Shares");
        }
    }
}
