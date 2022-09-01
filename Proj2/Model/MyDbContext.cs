using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Proj2.Model
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aktywa> Aktywas { get; set; } = null!;
        public virtual DbSet<Notowanium> Notowania { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-9K2SBT4;Initial Catalog=Proj1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aktywa>(entity =>
            {
                entity.HasKey(e => e.Idaktywa);

                entity.ToTable("Aktywa");

                entity.Property(e => e.Idaktywa).HasColumnName("IDAktywa");

                entity.Property(e => e.KodAktywa)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.NazwaAktywa)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Notowanium>(entity =>
            {
                entity.HasKey(e => e.Idnotowania);

                entity.Property(e => e.Idnotowania).HasColumnName("IDNotowania");

                entity.Property(e => e.CenaMax).HasColumnType("money");

                entity.Property(e => e.CenaMin).HasColumnType("money");

                entity.Property(e => e.CenaOtwarcia).HasColumnType("money");

                entity.Property(e => e.CenaZamkniecia).HasColumnType("money");

                entity.Property(e => e.DataIgodzina)
                    .HasColumnType("datetime")
                    .HasColumnName("DataIGodzina");

                entity.Property(e => e.Idaktywa).HasColumnName("IDAktywa");

                entity.HasOne(d => d.IdaktywaNavigation)
                    .WithMany(p => p.Notowania)
                    .HasForeignKey(d => d.Idaktywa)
                    .HasConstraintName("FK_Notowania_Aktywa");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
