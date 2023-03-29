using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApplication.Migrations
{
    /// <inheritdoc />
    public partial class _29marchFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    blogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    previewImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    likes = table.Column<int>(type: "int", nullable: false),
                    dislikes = table.Column<int>(type: "int", nullable: false),
                    views = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.blogId);
                });

            migrationBuilder.CreateTable(
                name: "blogTagMaps",
                columns: table => new
                {
                    mapId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    blogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blogTagMaps", x => x.mapId);
                });

            migrationBuilder.CreateTable(
                name: "BLTokens",
                columns: table => new
                {
                    tokenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BLTokens", x => x.tokenId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    phoneNo = table.Column<long>(type: "bigint", nullable: false),
                    dateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastActive = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false),
                    ProfileImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "ProfileImagePath", "created", "dateOfBirth", "email", "firstName", "isAdmin", "isDeleted", "lastActive", "lastName", "password", "phoneNo", "updated" },
                values: new object[] { new Guid("037c55b4-1681-4c59-95b9-4028b49e4b55"), "", new DateTime(2023, 3, 29, 16, 50, 57, 409, DateTimeKind.Local).AddTicks(7783), new DateTime(2023, 3, 29, 16, 50, 57, 409, DateTimeKind.Local).AddTicks(7775), "admin@gmail.com", "Admin", true, false, new DateTime(2023, 3, 29, 16, 50, 57, 409, DateTimeKind.Local).AddTicks(7784), "Admin", new byte[] { 48, 56, 57, 55, 50, 51, 52, 48, 54, 50, 66, 67, 70, 48, 68, 68, 52, 65, 68, 48, 68, 66, 49, 48, 52, 54, 56, 54, 48, 65, 49, 48, 51, 57, 53, 51, 70, 50, 68, 65, 56, 54, 68, 49, 49, 50, 65, 56, 54, 48, 70, 69, 52, 66, 51, 50, 70, 57, 57, 56, 50, 52, 57, 56, 57, 65, 69, 66, 67, 48, 51, 53, 55, 54, 68, 55, 65, 55, 50, 51, 57, 55, 66, 68, 66, 52, 55, 53, 56, 53, 49, 54, 65, 54, 48, 50, 69, 65, 48, 67, 54, 57, 49, 65, 67, 68, 51, 50, 55, 69, 56, 54, 70, 52, 68, 67, 48, 66, 49, 50, 65, 56, 65, 55, 56, 68, 57, 48 }, 9888636009L, new DateTime(2023, 3, 29, 16, 50, 57, 409, DateTimeKind.Local).AddTicks(7784) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "blogTagMaps");

            migrationBuilder.DropTable(
                name: "BLTokens");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
