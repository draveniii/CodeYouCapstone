using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVCWebBanking.Migrations
{
    /// <inheritdoc />
    public partial class Added_Full_Database_Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "DateOfBirth", "Name", "SSNumber" },
                values: new object[,]
                {
                    { 1, new DateOnly(2001, 5, 18), "Draven McConathy", 123456789 },
                    { 2, new DateOnly(2002, 3, 12), "Mercedes Helliangao", 987654321 },
                    { 3, new DateOnly(1987, 5, 1), "Terry Pratchet", 278465792 },
                    { 4, new DateOnly(1950, 10, 13), "Harry Dresdon", 98764245 },
                    { 5, new DateOnly(2004, 1, 27), "Adam Littlefinger", 98536678 },
                    { 6, new DateOnly(1979, 8, 19), "Mary Littlefinger", 444886578 }
                });

            migrationBuilder.InsertData(
                table: "Shares",
                columns: new[] { "Id", "AccountId", "CurrentBalance", "InterestRate", "MinimumBalance", "Type" },
                values: new object[,]
                {
                    { 4, 2, 2500m, 0.005m, 25m, "Savings" },
                    { 5, 2, 400m, 0m, 0m, "Checking" },
                    { 6, 3, 1500m, 0.005m, 25m, "Savings" },
                    { 7, 4, 25m, 0.005m, 25m, "Savings" },
                    { 8, 4, 325m, 0m, 0m, "Checking" },
                    { 9, 4, 10000m, 0.1m, 1000m, "Money Market" },
                    { 10, 5, 2500m, 0.005m, 25m, "Savings" },
                    { 11, 5, 400m, 0.1m, 1000m, "Money Market" }
                });

            migrationBuilder.InsertData(
                table: "AccountMember",
                columns: new[] { "AccountMemberId", "AccountId", "MemberId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 2 },
                    { 4, 3, 3 },
                    { 5, 4, 4 },
                    { 6, 5, 4 },
                    { 7, 5, 5 },
                    { 8, 5, 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountMember",
                keyColumn: "AccountMemberId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AccountMember",
                keyColumn: "AccountMemberId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AccountMember",
                keyColumn: "AccountMemberId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AccountMember",
                keyColumn: "AccountMemberId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AccountMember",
                keyColumn: "AccountMemberId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AccountMember",
                keyColumn: "AccountMemberId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AccountMember",
                keyColumn: "AccountMemberId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AccountMember",
                keyColumn: "AccountMemberId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Shares",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Shares",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Shares",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Shares",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Shares",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Shares",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Shares",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Shares",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
