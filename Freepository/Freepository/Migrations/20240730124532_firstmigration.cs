using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Freepository.Migrations
{
    /// <inheritdoc />
    public partial class firstmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1332659c-08fd-431a-ac55-362b5c8a7844");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a32bf5c-f980-408a-a0de-4eb8c03b668c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4f441919-4aab-49a8-a551-90991713a2c0", null, "Admin", "ADMIN" },
                    { "50fcc71f-823f-4a6d-8eaa-f0bc9bd5d43c", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f441919-4aab-49a8-a551-90991713a2c0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50fcc71f-823f-4a6d-8eaa-f0bc9bd5d43c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1332659c-08fd-431a-ac55-362b5c8a7844", null, "Admin", "ADMIN" },
                    { "5a32bf5c-f980-408a-a0de-4eb8c03b668c", null, "User", "USER" }
                });
        }
    }
}
