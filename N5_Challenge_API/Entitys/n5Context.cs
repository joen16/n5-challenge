using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace N5_Challenge_API.Entitys
{
    public partial class n5Context : DbContext
    {
        private readonly IConfiguration _configuration;
        public n5Context()
        {
        }

        public n5Context(DbContextOptions<n5Context> options, IConfiguration configuration)
            : base(options)
        {
            this._configuration = configuration;
        }

        public virtual DbSet<employee> employee { get; set; } = null!;
        public virtual DbSet<permision> permision { get; set; } = null!;
        public virtual DbSet<permission_type> permission_type { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string cx = this._configuration.GetConnectionString("n5");
                optionsBuilder.UseSqlServer(cx);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<employee>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<permision>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.permision)
                    .HasForeignKey(d => d.IdEmployee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_permision_employee");

                entity.HasOne(d => d.IdPermissionTypeNavigation)
                    .WithMany(p => p.permision)
                    .HasForeignKey(d => d.IdPermissionType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_permision_permission_type");
            });

            modelBuilder.Entity<permission_type>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
