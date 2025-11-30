using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using laba1s3core;
namespace DataAccessLayer
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<Book> Books { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dataDir = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
            if (string.IsNullOrEmpty(dataDir)) AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetCurrentDirectory());
            var conn = "Server=(localdb)\\MSSQLLocalDB;Database=LibraryDb;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(conn);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<Book>().HasKey(b => b.Id);
        }
    }
}
