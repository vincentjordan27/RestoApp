using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestoApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c658ac3d-bb18-4a46-a9d5-610774a21c62", "c658ac3d-bb18-4a46-a9d5-610774a21c62", "Resto", "RESTO" },
                    { "d193a029-36ab-478b-bf89-3302bfb34f53", "d193a029-36ab-478b-bf89-3302bfb34f53", "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c658ac3d-bb18-4a46-a9d5-610774a21c62");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d193a029-36ab-478b-bf89-3302bfb34f53");
        }
    }
}
