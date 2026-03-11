using Microsoft.EntityFrameworkCore;
using JobSeekerApi.Models;

namespace JobSeekerApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Role).HasDefaultValue("JobSeeker");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasKey(j => j.JobId);
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasKey(a => a.ApplicationId);
                entity.Property(a => a.Status).HasDefaultValue("Applied");

                entity.HasOne(a => a.Job)
                      .WithMany()
                      .HasForeignKey(a => a.JobId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(a => a.User)
                      .WithMany()
                      .HasForeignKey(a => a.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}