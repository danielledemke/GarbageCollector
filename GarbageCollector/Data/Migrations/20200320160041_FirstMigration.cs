using Microsoft.EntityFrameworkCore.Migrations;

namespace GarbageCollector.Data.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a34d3f71-b329-4028-ba6e-d9037c6d8a68");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "77817b7c-67c3-471f-9c3f-05349f995bac", "61d25665-4f77-4ebf-9f41-59d65609b180", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77817b7c-67c3-471f-9c3f-05349f995bac");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a34d3f71-b329-4028-ba6e-d9037c6d8a68", "ecae554f-d3b6-40d3-9d57-f49870c0bf41", "Admin", "ADMIN" });
        }
    }
}
