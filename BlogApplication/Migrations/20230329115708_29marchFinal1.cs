using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApplication.Migrations
{
    /// <inheritdoc />
    public partial class _29marchFinal1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("037c55b4-1681-4c59-95b9-4028b49e4b55"));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "ProfileImagePath", "created", "dateOfBirth", "email", "firstName", "isAdmin", "isDeleted", "lastActive", "lastName", "password", "phoneNo", "updated" },
                values: new object[] { new Guid("4c0eb5ed-ab44-4008-9683-e57333636085"), "", new DateTime(2023, 3, 29, 17, 27, 8, 94, DateTimeKind.Local).AddTicks(9461), new DateTime(2023, 3, 29, 17, 27, 8, 94, DateTimeKind.Local).AddTicks(9453), "admin@gmail.com", "Admin", true, false, new DateTime(2023, 3, 29, 17, 27, 8, 94, DateTimeKind.Local).AddTicks(9462), "Admin", new byte[] { 48, 56, 57, 55, 50, 51, 52, 48, 54, 50, 66, 67, 70, 48, 68, 68, 52, 65, 68, 48, 68, 66, 49, 48, 52, 54, 56, 54, 48, 65, 49, 48, 51, 57, 53, 51, 70, 50, 68, 65, 56, 54, 68, 49, 49, 50, 65, 56, 54, 48, 70, 69, 52, 66, 51, 50, 70, 57, 57, 56, 50, 52, 57, 56, 57, 65, 69, 66, 67, 48, 51, 53, 55, 54, 68, 55, 65, 55, 50, 51, 57, 55, 66, 68, 66, 52, 55, 53, 56, 53, 49, 54, 65, 54, 48, 50, 69, 65, 48, 67, 54, 57, 49, 65, 67, 68, 51, 50, 55, 69, 56, 54, 70, 52, 68, 67, 48, 66, 49, 50, 65, 56, 65, 55, 56, 68, 57, 48 }, 9888636009L, new DateTime(2023, 3, 29, 17, 27, 8, 94, DateTimeKind.Local).AddTicks(9462) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("4c0eb5ed-ab44-4008-9683-e57333636085"));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "ProfileImagePath", "created", "dateOfBirth", "email", "firstName", "isAdmin", "isDeleted", "lastActive", "lastName", "password", "phoneNo", "updated" },
                values: new object[] { new Guid("037c55b4-1681-4c59-95b9-4028b49e4b55"), "", new DateTime(2023, 3, 29, 16, 50, 57, 409, DateTimeKind.Local).AddTicks(7783), new DateTime(2023, 3, 29, 16, 50, 57, 409, DateTimeKind.Local).AddTicks(7775), "admin@gmail.com", "Admin", true, false, new DateTime(2023, 3, 29, 16, 50, 57, 409, DateTimeKind.Local).AddTicks(7784), "Admin", new byte[] { 48, 56, 57, 55, 50, 51, 52, 48, 54, 50, 66, 67, 70, 48, 68, 68, 52, 65, 68, 48, 68, 66, 49, 48, 52, 54, 56, 54, 48, 65, 49, 48, 51, 57, 53, 51, 70, 50, 68, 65, 56, 54, 68, 49, 49, 50, 65, 56, 54, 48, 70, 69, 52, 66, 51, 50, 70, 57, 57, 56, 50, 52, 57, 56, 57, 65, 69, 66, 67, 48, 51, 53, 55, 54, 68, 55, 65, 55, 50, 51, 57, 55, 66, 68, 66, 52, 55, 53, 56, 53, 49, 54, 65, 54, 48, 50, 69, 65, 48, 67, 54, 57, 49, 65, 67, 68, 51, 50, 55, 69, 56, 54, 70, 52, 68, 67, 48, 66, 49, 50, 65, 56, 65, 55, 56, 68, 57, 48 }, 9888636009L, new DateTime(2023, 3, 29, 16, 50, 57, 409, DateTimeKind.Local).AddTicks(7784) });
        }
    }
}
