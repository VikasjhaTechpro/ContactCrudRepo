using ContactApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Configure model here if needed
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.FullName).IsRequired().HasMaxLength(200);
                b.Property(x => x.Email).IsRequired().HasMaxLength(200);
                b.Property(x => x.Phone).HasMaxLength(50);
                b.Property(x => x.Address).HasMaxLength(1000);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
