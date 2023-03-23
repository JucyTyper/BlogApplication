using BlogApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Data
{
    public class blogAppDatabase : DbContext
    {
        public blogAppDatabase(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserModel> users { get; set; }

    }
}
