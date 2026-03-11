using JobPortal.AdminService.Models;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.AdminService.Data
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
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Application>()
                .HasOne(a => a.User)
                .WithMany(u => u.Applications)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.Job)
                .WithMany(j => j.Applications)
                .HasForeignKey(a => a.JobId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
                Name = "Admin",
                Email = "admin@gmail.com",
                PasswordHash = "$2a$11$QHrwvsquG9jFPQZ7oZ71Mue9GFeX58yg4m7dPG7fpqS8wAvbx6lV2",
                Role = "Admin",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });


            modelBuilder.Entity<Job>().HasData(
                new Job
                {
                    JobId = 1,
                    JobTitle = "Software Engineer",
                    CompanyName = "HCL Technologies",
                    JobDescription = "Develop and maintain web applications using .NET Core and Angular.",
                    Location = "Chennai",
                    SalaryRange = "5LPA - 10LPA",
                    PostedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsActive = true
                },
                new Job
                {
                    JobId = 2,
                    JobTitle = "Frontend Developer",
                    CompanyName = "Infosys",
                    JobDescription = "Build responsive UI using Angular and TypeScript.",
                    Location = "Bangalore",
                    SalaryRange = "4LPA - 8LPA",
                    PostedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsActive = true
                }
            );
        }
    }
}