using Microsoft.EntityFrameworkCore;
using SimpleApi.Models;
using DomainTables;

namespace SimpleApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Існуюча сутність
        public DbSet<Product> Products { get; set; }

        // Нові сутності
        public DbSet<Author>    Authors    { get; set; }
        public DbSet<Book>      Books      { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // При потребі додаткова конфігурація Fluent API:
            // modelBuilder.Entity<Book>()
            //     .HasOne(b => b.Author)
            //     .WithMany(a => a.Books)
            //     .HasForeignKey(b => b.AuthorId);
        }
    }
}
