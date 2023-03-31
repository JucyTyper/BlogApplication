using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApplication.Migrations
{
    /// <inheritdoc />
    public partial class _31March3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("6cc02534-fdbe-46ac-9aeb-2936832c5043"));

            migrationBuilder.AddColumn<bool>(
                name: "isBlocked",
                table: "users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isBlocked",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "ProfileImagePath", "created", "dateOfBirth", "email", "firstName", "isAdmin", "isBlocked", "isDeleted", "lastActive", "lastName", "password", "phoneNo", "updated" },
                values: new object[] { new Guid("46f804c9-1378-476f-85eb-8565b1bbc18f"), "", new DateTime(2023, 3, 31, 18, 33, 49, 393, DateTimeKind.Local).AddTicks(3827), new DateTime(2023, 3, 31, 18, 33, 49, 393, DateTimeKind.Local).AddTicks(3819), "admin@gmail.com", "Admin", true, false, false, new DateTime(2023, 3, 31, 18, 33, 49, 393, DateTimeKind.Local).AddTicks(3827), "Admin", new byte[] { 65, 100, 109, 105, 110, 64, 49, 50, 51 }, 9888636009L, new DateTime(2023, 3, 31, 18, 33, 49, 393, DateTimeKind.Local).AddTicks(3827) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("46f804c9-1378-476f-85eb-8565b1bbc18f"));

            migrationBuilder.DropColumn(
                name: "isBlocked",
                table: "users");

            migrationBuilder.DropColumn(
                name: "isBlocked",
                table: "Blogs");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "ProfileImagePath", "created", "dateOfBirth", "email", "firstName", "isAdmin", "isDeleted", "lastActive", "lastName", "password", "phoneNo", "updated" },
                values: new object[] { new Guid("6cc02534-fdbe-46ac-9aeb-2936832c5043"), "", new DateTime(2023, 3, 31, 12, 17, 11, 22, DateTimeKind.Local).AddTicks(2280), new DateTime(2023, 3, 31, 12, 17, 11, 22, DateTimeKind.Local).AddTicks(2269), "admin@gmail.com", "Admin", true, false, new DateTime(2023, 3, 31, 12, 17, 11, 22, DateTimeKind.Local).AddTicks(2281), "Admin", new byte[] { 65, 100, 109, 105, 110, 64, 49, 50, 51 }, 9888636009L, new DateTime(2023, 3, 31, 12, 17, 11, 22, DateTimeKind.Local).AddTicks(2280) });
        }
    }
}
