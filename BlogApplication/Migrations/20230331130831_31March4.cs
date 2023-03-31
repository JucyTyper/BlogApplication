using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApplication.Migrations
{
    /// <inheritdoc />
    public partial class _31March4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("46f804c9-1378-476f-85eb-8565b1bbc18f"));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "ProfileImagePath", "created", "dateOfBirth", "email", "firstName", "isAdmin", "isBlocked", "isDeleted", "lastActive", "lastName", "password", "phoneNo", "updated" },
                values: new object[] { new Guid("7283602d-635e-45d4-8e74-4f0287290343"), "", new DateTime(2023, 3, 31, 18, 38, 31, 570, DateTimeKind.Local).AddTicks(4654), new DateTime(2023, 3, 31, 18, 38, 31, 570, DateTimeKind.Local).AddTicks(4641), "admin@gmail.com", "Admin", true, false, false, new DateTime(2023, 3, 31, 18, 38, 31, 570, DateTimeKind.Local).AddTicks(4656), "Admin", new byte[] { 65, 100, 109, 105, 110, 64, 49, 50, 51 }, 9888636009L, new DateTime(2023, 3, 31, 18, 38, 31, 570, DateTimeKind.Local).AddTicks(4656) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("7283602d-635e-45d4-8e74-4f0287290343"));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "ProfileImagePath", "created", "dateOfBirth", "email", "firstName", "isAdmin", "isBlocked", "isDeleted", "lastActive", "lastName", "password", "phoneNo", "updated" },
                values: new object[] { new Guid("46f804c9-1378-476f-85eb-8565b1bbc18f"), "", new DateTime(2023, 3, 31, 18, 33, 49, 393, DateTimeKind.Local).AddTicks(3827), new DateTime(2023, 3, 31, 18, 33, 49, 393, DateTimeKind.Local).AddTicks(3819), "admin@gmail.com", "Admin", true, false, false, new DateTime(2023, 3, 31, 18, 33, 49, 393, DateTimeKind.Local).AddTicks(3827), "Admin", new byte[] { 65, 100, 109, 105, 110, 64, 49, 50, 51 }, 9888636009L, new DateTime(2023, 3, 31, 18, 33, 49, 393, DateTimeKind.Local).AddTicks(3827) });
        }
    }
}
