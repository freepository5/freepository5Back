using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Freepository.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigrationAfterFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01714d6a-1cca-4663-b6eb-839a07ff3b2c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9065d6c-f729-4a44-9bcc-884345d9dd63");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "89261e4a-fdc0-4322-9d40-cfda7246a390", null, "Admin", "ADMIN" },
                    { "ac378c80-9eb0-44dd-8a80-3191b1bd272f", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89261e4a-fdc0-4322-9d40-cfda7246a390");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac378c80-9eb0-44dd-8a80-3191b1bd272f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01714d6a-1cca-4663-b6eb-839a07ff3b2c", null, "Admin", "ADMIN" },
                    { "b9065d6c-f729-4a44-9bcc-884345d9dd63", null, "User", "USER" }
                });
        }
    }
}
