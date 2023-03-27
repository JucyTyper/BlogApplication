using BlogApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace BlogApplication.Data
{
    public class blogAppDatabase : DbContext
    {
        public blogAppDatabase(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserModel> users { get; set; }
        public DbSet<BlackListTokenModel> BLTokens { get; set; }
        public DbSet<BlogModel> Blogs { get; set; }
        public DbSet<TagsModel> Tags { get; set; }
    }
}
