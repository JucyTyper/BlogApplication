using BlogApplication.Models;
using BlogApplication.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace BlogApplication.Data
{
    public class blogAppDatabase : DbContext
    {
        public blogAppDatabase(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder) 
        {
            builder.Entity<UserModel>().HasData(
                new UserModel
                {
                    firstName = "Admin",
                    lastName = "Admin",
                    email = "admin@gmail.com",
                    password = Encoding.ASCII.GetBytes("Admin@123"),
                    phoneNo = 9888636009,
                    isAdmin = true,
                });
        }
        public DbSet<UserModel> users { get; set; }
        public DbSet<BlackListTokenModel> BLTokens { get; set; }
        public DbSet<BlogModel> Blogs { get; set; }
        public DbSet<TagsModel> Tags { get; set; }
        public DbSet<BlogTagMap> blogTagMaps { get; set; }
        public DbSet<NoticeModel> notices { get; set; }
    }
}
