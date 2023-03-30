﻿// <auto-generated />
using System;
using BlogApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlogApplication.Migrations
{
    [DbContext(typeof(blogAppDatabase))]
    [Migration("20230330124651_30March")]
    partial class _30March
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BlogApplication.Models.BlackListTokenModel", b =>
                {
                    b.Property<Guid>("tokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("tokenId");

                    b.ToTable("BLTokens");
                });

            modelBuilder.Entity("BlogApplication.Models.BlogModel", b =>
                {
                    b.Property<Guid>("blogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("createrId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("dislikes")
                        .HasColumnType("int");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("likes")
                        .HasColumnType("int");

                    b.Property<string>("previewImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("views")
                        .HasColumnType("int");

                    b.HasKey("blogId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("BlogApplication.Models.BlogTagMap", b =>
                {
                    b.Property<Guid>("mapId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("blogId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("tagId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("mapId");

                    b.ToTable("blogTagMaps");
                });

            modelBuilder.Entity("BlogApplication.Models.NoticeModel", b =>
                {
                    b.Property<Guid>("noticeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("noticeData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("noticeId");

                    b.ToTable("notices");
                });

            modelBuilder.Entity("BlogApplication.Models.TagsModel", b =>
                {
                    b.Property<Guid>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("BlogApplication.Models.UserModel", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProfileImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("lastActive")
                        .HasColumnType("datetime2");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<long>("phoneNo")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("updated")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("a218b18f-0c3f-4570-82f9-a0feb92fab6f"),
                            ProfileImagePath = "",
                            created = new DateTime(2023, 3, 30, 18, 16, 51, 607, DateTimeKind.Local).AddTicks(1237),
                            dateOfBirth = new DateTime(2023, 3, 30, 18, 16, 51, 607, DateTimeKind.Local).AddTicks(1228),
                            email = "admin@gmail.com",
                            firstName = "Admin",
                            isAdmin = true,
                            isDeleted = false,
                            lastActive = new DateTime(2023, 3, 30, 18, 16, 51, 607, DateTimeKind.Local).AddTicks(1237),
                            lastName = "Admin",
                            password = new byte[] { 65, 100, 109, 105, 110, 64, 49, 50, 51 },
                            phoneNo = 9888636009L,
                            updated = new DateTime(2023, 3, 30, 18, 16, 51, 607, DateTimeKind.Local).AddTicks(1237)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}