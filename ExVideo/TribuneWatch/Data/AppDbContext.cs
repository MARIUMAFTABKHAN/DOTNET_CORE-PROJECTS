using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using TribuneWatch.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    //public DbSet<VideoSearchRow> VideoSearchRows => Set<VideoSearchRow>();

    public DbSet<VideoSearchRow> VideoSearchRows { get; set; } = default!;
    public DbSet<User> Users { get; set; }

    //protected override void OnModelCreating(ModelBuilder mb)
    //{
    //    mb.Entity<VideoSearchRow>().HasNoKey().ToView(null);
    //    base.OnModelCreating(mb);
    //}

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<VideoSearchRow>(eb =>
        {
            eb.HasNoKey();
            eb.ToView((string?)null); 
        });

        mb.Entity<User>().ToTable("tblUser_JTSImported");

        base.OnModelCreating(mb);
    }
}
        