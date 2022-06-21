using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Restaurant.Models
{
    public partial class RestaurantManagerContext : DbContext
    {
        public RestaurantManagerContext()
        {
        }

        public RestaurantManagerContext(DbContextOptions<RestaurantManagerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmployeeWorkDay> EmployeeWorkDays { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Supply> Supplies { get; set; } = null!;
        public virtual DbSet<SupplyProduct> SupplyProducts { get; set; } = null!;
        public virtual DbSet<SupplyStatus> SupplyStatuses { get; set; } = null!;
        public virtual DbSet<TableReserve> TableReserves { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<WorkDay> WorkDays { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-EG5H0MG\\SQLEXPRESS;Database=RestaurantManager;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Id).HasColumnType("int");

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Post");
            });

            modelBuilder.Entity<EmployeeWorkDay>(entity =>
            {
                entity.ToTable("EmployeeWorkDay");

                entity.Property(e => e.Id).HasColumnType("int");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeWorkDays)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeWorkDay_Employee");

                entity.HasOne(d => d.WorkDay)
                    .WithMany(p => p.EmployeeWorkDays)
                    .HasForeignKey(d => d.WorkDayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeWorkDay_WorkDay");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.Id).HasColumnType("int");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnType("int");

                entity.Property(e => e.Id).HasColumnType("int");

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.Unit).HasMaxLength(5);
            });


            modelBuilder.Entity<Supply>(entity =>
            {
                entity.ToTable("Supply");

                entity.Property(e => e.Id).HasColumnType("int");

                entity.Property(e => e.Date).HasColumnType("date");
            });

            modelBuilder.Entity<SupplyProduct>(entity =>
            {
                entity.ToTable("SupplyProduct");
                entity.Property(e => e.Id).HasColumnType("int");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SupplyProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplyProduct_Product");

                entity.HasOne(d => d.Supply)
                    .WithMany(p => p.SupplyProducts)
                    .HasForeignKey(d => d.SupplyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplyProduct_Supply");

                entity.HasOne(d => d.SupplyNavigation)
                    .WithMany(p => p.SupplyProducts)
                    .HasForeignKey(d => d.SupplyStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplyProduct_SupplyStatus");
            });

            modelBuilder.Entity<SupplyStatus>(entity =>
            {
                entity.ToTable("SupplyStatus");

                entity.Property(e => e.Id).HasColumnType("int");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<TableReserve>(entity =>
            {
                entity.ToTable("TableReserve");

                entity.Property(e => e.Id).HasColumnType("int");

                entity.Property(e => e.CustomerName).HasMaxLength(50);

                entity.Property(e => e.CustomerPhone).HasMaxLength(50);

                entity.Property(e => e.DateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int");

                entity.ToTable("User");

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.FullName).HasMaxLength(150);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(20);
            });

            modelBuilder.Entity<WorkDay>(entity =>
            {
                entity.ToTable("WorkDay");

                entity.Property(e => e.Id).HasColumnType("int");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.EveningCash).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.MorningCash).HasColumnType("decimal(10, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
