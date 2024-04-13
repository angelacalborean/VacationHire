using Microsoft.EntityFrameworkCore;
using VacationHire.Data.Models;

namespace VacationHire.Data.Data
{
    /// <summary>
    ///     Database context for the VacationHire application
    /// </summary>
    public class VacationHireDbContext : DbContext
    {
        /// <summary>
        ///     Used in unit tests
        /// </summary>
        public VacationHireDbContext() { }

        public VacationHireDbContext(DbContextOptions<VacationHireDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Asset> Assets { get; set; }

        public virtual DbSet<CabinAsset> CabinAssets { get; set; }

        public virtual DbSet<CarAsset> CarAssets { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.AssetName)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(250);

                entity.HasOne(d => d.Category).WithMany(p => p.Assets)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CabinAsset>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Address).HasMaxLength(250);
                entity.Property(e => e.Description).HasMaxLength(250);

                entity.HasOne(d => d.Asset).WithMany(p => p.CabinAssets)
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CarAsset>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(d => d.Asset).WithMany(p => p.CarAssets)
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CategoryName).HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(250);
            });
        }
    }
}