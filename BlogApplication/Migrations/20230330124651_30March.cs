using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApplication.Migrations
{
    /// <inheritdoc />
    public partial class _30March : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("4c0eb5ed-ab44-4008-9683-e57333636085"));

            migrationBuilder.CreateTable(
                name: "notices",
                columns: table => new
                {
                    noticeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    noticeData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notices", x => x.noticeId);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "ProfileImagePath", "created", "dateOfBirth", "email", "firstName", "isAdmin", "isDeleted", "lastActive", "lastName", "password", "phoneNo", "updated" },
                values: new object[] { new Guid("a218b18f-0c3f-4570-82f9-a0feb92fab6f"), "", new DateTime(2023, 3, 30, 18, 16, 51, 607, DateTimeKind.Local).AddTicks(1237), new DateTime(2023, 3, 30, 18, 16, 51, 607, DateTimeKind.Local).AddTicks(1228), "admin@gmail.com", "Admin", true, false, new DateTime(2023, 3, 30, 18, 16, 51, 607, DateTimeKind.Local).AddTicks(1237), "Admin", new byte[] { 65, 100, 109, 105, 110, 64, 49, 50, 51 }, 9888636009L, new DateTime(2023, 3, 30, 18, 16, 51, 607, DateTimeKind.Local).AddTicks(1237) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notices");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("a218b18f-0c3f-4570-82f9-a0feb92fab6f"));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "ProfileImagePath", "created", "dateOfBirth", "email", "firstName", "isAdmin", "isDeleted", "lastActive", "lastName", "password", "phoneNo", "updated" },
                values: new object[] { new Guid("4c0eb5ed-ab44-4008-9683-e57333636085"), "", new DateTime(2023, 3, 29, 17, 27, 8, 94, DateTimeKind.Local).AddTicks(9461), new DateTime(2023, 3, 29, 17, 27, 8, 94, DateTimeKind.Local).AddTicks(9453), "admin@gmail.com", "Admin", true, false, new DateTime(2023, 3, 29, 17, 27, 8, 94, DateTimeKind.Local).AddTicks(9462), "Admin", new byte[] { 48, 56, 57, 55, 50, 51, 52, 48, 54, 50, 66, 67, 70, 48, 68, 68, 52, 65, 68, 48, 68, 66, 49, 48, 52, 54, 56, 54, 48, 65, 49, 48, 51, 57, 53, 51, 70, 50, 68, 65, 56, 54, 68, 49, 49, 50, 65, 56, 54, 48, 70, 69, 52, 66, 51, 50, 70, 57, 57, 56, 50, 52, 57, 56, 57, 65, 69, 66, 67, 48, 51, 53, 55, 54, 68, 55, 65, 55, 50, 51, 57, 55, 66, 68, 66, 52, 55, 53, 56, 53, 49, 54, 65, 54, 48, 50, 69, 65, 48, 67, 54, 57, 49, 65, 67, 68, 51, 50, 55, 69, 56, 54, 70, 52, 68, 67, 48, 66, 49, 50, 65, 56, 65, 55, 56, 68, 57, 48 }, 9888636009L, new DateTime(2023, 3, 29, 17, 27, 8, 94, DateTimeKind.Local).AddTicks(9462) });
        }
    }
}
