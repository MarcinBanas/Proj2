﻿using System;
using System.Collections.Generic;
using System.Configuration;
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
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["ProjBaza"].ConnectionString);
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
