using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UserAndMsg.Models
{
    public partial class TestLogContext : DbContext
    {
        public TestLogContext()
        {
        }

        public TestLogContext(DbContextOptions<TestLogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<LogItem> LogItems { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=TestLog;Trusted_Connection=True;User ID=sa;Password=00000000");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Path)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("path");

                entity.Property(e => e.Time)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("time");
            });

            modelBuilder.Entity<LogItem>(entity =>
            {
                entity.ToTable("LogItem");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LogMsg)
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Account);

                entity.ToTable("User");

                entity.Property(e => e.Account)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Permission)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Time)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Path)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Path");
            });

            OnModelCreatingPartial(modelBuilder);
        }



        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
