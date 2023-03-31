﻿// <auto-generated />
using System;
using BlogApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlogApplication.Migrations
{
    [DbContext(typeof(blogAppDatabase))]
    partial class blogAppDatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<bool>("isBlocked")
                        .HasColumnType("bit");

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

            modelBuilder.Entity("BlogApplication.Models.LikeAndDislikeMapModel", b =>
                {
                    b.Property<Guid>("activityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("blogId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("isDisliked")
                        .HasColumnType("bit");

                    b.Property<bool>("isLiked")
                        .HasColumnType("bit");

                    b.Property<Guid>("userId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("activityId");

                    b.ToTable("LikeAndDislikes");
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

                    b.Property<bool>("isBlocked")
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
                            UserId = new Guid("7283602d-635e-45d4-8e74-4f0287290343"),
                            ProfileImagePath = "",
                            created = new DateTime(2023, 3, 31, 18, 38, 31, 570, DateTimeKind.Local).AddTicks(4654),
                            dateOfBirth = new DateTime(2023, 3, 31, 18, 38, 31, 570, DateTimeKind.Local).AddTicks(4641),
                            email = "admin@gmail.com",
                            firstName = "Admin",
                            isAdmin = true,
                            isBlocked = false,
                            isDeleted = false,
                            lastActive = new DateTime(2023, 3, 31, 18, 38, 31, 570, DateTimeKind.Local).AddTicks(4656),
                            lastName = "Admin",
                            password = new byte[] { 65, 100, 109, 105, 110, 64, 49, 50, 51 },
                            phoneNo = 9888636009L,
                            updated = new DateTime(2023, 3, 31, 18, 38, 31, 570, DateTimeKind.Local).AddTicks(4656)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
