using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using WEB_153505_PIKHTOVNIKAVA.Domain.Entities;

namespace WEB_153505_PIKHTOVNIKAVA.API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Sneaker> sneakers { get; set; }
        public DbSet<SeasonCategory> season { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sneaker>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.SeasonCategory)
                    .WithMany(e => e.Sneakers)
                    .HasForeignKey(e => e.SeasonCategoryId);
            });

            modelBuilder.Entity<SeasonCategory>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasMany(e => e.Sneakers)
                    .WithOne(e => e.SeasonCategory)
                    .HasForeignKey(e => e.SeasonCategoryId);
            });
        }
    }
}
    