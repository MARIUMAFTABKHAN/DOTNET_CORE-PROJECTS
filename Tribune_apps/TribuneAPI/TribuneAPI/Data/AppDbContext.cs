using TribuneAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace TribuneAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<VideoSearchRow> VideoSearchRows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VideoSearchRow>().HasNoKey(); // ✅ Because it's a stored proc result
            base.OnModelCreating(modelBuilder);
        }
    }
}
