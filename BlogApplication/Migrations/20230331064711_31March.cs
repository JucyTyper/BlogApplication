using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApplication.Migrations
{
    /// <inheritdoc />
    public partial class _31March : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("a218b18f-0c3f-4570-82f9-a0feb92fab6f"));

            migrationBuilder.CreateTable(
                name: "LikeAndDislikes",
                columns: table => new
                {
                    activityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    blogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isLiked = table.Column<bool>(type: "bit", nullable: false),
                    isDisliked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeAndDislikes", x => x.activityId);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "ProfileImagePath", "created", "dateOfBirth", "email", "firstName", "isAdmin", "isDeleted", "lastActive", "lastName", "password", "phoneNo", "updated" },
                values: new object[] { new Guid("6cc02534-fdbe-46ac-9aeb-2936832c5043"), "", new DateTime(2023, 3, 31, 12, 17, 11, 22, DateTimeKind.Local).AddTicks(2280), new DateTime(2023, 3, 31, 12, 17, 11, 22, DateTimeKind.Local).AddTicks(2269), "admin@gmail.com", "Admin", true, false, new DateTime(2023, 3, 31, 12, 17, 11, 22, DateTimeKind.Local).AddTicks(2281), "Admin", new byte[] { 65, 100, 109, 105, 110, 64, 49, 50, 51 }, 9888636009L, new DateTime(2023, 3, 31, 12, 17, 11, 22, DateTimeKind.Local).AddTicks(2280) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeAndDislikes");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "UserId",
                keyValue: new Guid("6cc02534-fdbe-46ac-9aeb-2936832c5043"));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "ProfileImagePath", "created", "dateOfBirth", "email", "firstName", "isAdmin", "isDeleted", "lastActive", "lastName", "password", "phoneNo", "updated" },
                values: new object[] { new Guid("a218b18f-0c3f-4570-82f9-a0feb92fab6f"), "", new DateTime(2023, 3, 30, 18, 16, 51, 607, DateTimeKind.Local).AddTicks(1237), new DateTime(2023, 3, 30, 18, 16, 51, 607, DateTimeKind.Local).AddTicks(1228), "admin@gmail.com", "Admin", true, false, new DateTime(2023, 3, 30, 18, 16, 51, 607, DateTimeKind.Local).AddTicks(1237), "Admin", new byte[] { 65, 100, 109, 105, 110, 64, 49, 50, 51 }, 9888636009L, new DateTime(2023, 3, 30, 18, 16, 51, 607, DateTimeKind.Local).AddTicks(1237) });
        }
    }
}
