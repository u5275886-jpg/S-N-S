using System;
using System.IO;
using CollegeIdManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeIdManagement.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Session> Sessions { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<CollegeSettings> Settings { get; set; } = null!;
        public DbSet<IdCardTemplate> IdCardTemplates { get; set; } = null!;
        public DbSet<AuditLog> AuditLogs { get; set; } = null!;

        private readonly string _dbPath;

        public AppDbContext()
        {
            var appDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "CollegeIdManagement",
                "Database");

            if (!Directory.Exists(appDataFolder))
            {
                Directory.CreateDirectory(appDataFolder);
            }

            _dbPath = Path.Combine(appDataFolder, "college.db");
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            _dbPath = string.Empty;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source={_dbPath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.MemberCode).IsUnique();
                entity.HasIndex(e => e.RollNo);
                entity.HasIndex(e => e.Name);
                entity.HasIndex(e => e.Mobile);
                entity.HasIndex(e => e.Session);
                entity.HasIndex(e => e.Course);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Username).IsUnique();
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Code).IsUnique();
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Code).IsUnique();
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Name).IsUnique();
            });
        }
    }
}
