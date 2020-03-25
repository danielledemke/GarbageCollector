using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GarbageCollector.Data.Migrations
{
    public partial class CustomerNullableValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81b23074-6784-4753-9fff-a02add9e6adb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb9807cc-717b-4e79-b808-1cb875e15e52");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa997ed4-5421-4295-96f2-92f7ef2ed224");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExtraPickupDay",
                table: "Customer",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Customer",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "07925f6a-0944-40a1-894a-87515e59ff49", "7d836c3b-e56a-4edd-b002-23acc4631849", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6c0374d3-f3d5-452a-b267-09f313dc1630", "0117f2c6-4d0a-4379-abc2-970fe40b6d0b", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8325db60-48de-4a2e-94cb-0108309f3995", "d5f963d9-b34e-434b-80fe-573a33d88260", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "07925f6a-0944-40a1-894a-87515e59ff49");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c0374d3-f3d5-452a-b267-09f313dc1630");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8325db60-48de-4a2e-94cb-0108309f3995");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExtraPickupDay",
                table: "Customer",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Customer",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "81b23074-6784-4753-9fff-a02add9e6adb", "5efe16ba-f47b-4be6-a3bf-03cf28e0e2d5", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fa997ed4-5421-4295-96f2-92f7ef2ed224", "3ea1acbf-a14e-40df-93f7-319de2640bf3", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eb9807cc-717b-4e79-b808-1cb875e15e52", "e4aafc2b-a65f-446d-b61f-8bf0bf956a74", "Employee", "EMPLOYEE" });
        }
    }
}
