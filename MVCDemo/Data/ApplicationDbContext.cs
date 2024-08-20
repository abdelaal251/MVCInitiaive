using Microsoft.EntityFrameworkCore;
using MVCDemo.Models;

namespace MVCDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<CourseResult> CourseResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relationships
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Courses)
                .WithOne(c => c.Department)
                .HasForeignKey(c => c.DeptId);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Instructors)
                .WithOne(i => i.Department)
                .HasForeignKey(i => i.DeptId);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Trainees)
                .WithOne(t => t.Department)
                .HasForeignKey(t => t.DeptId);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Instructors)
                .WithOne(i => i.Course)
                .HasForeignKey(i => i.CourseId);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.CourseResults)
                .WithOne(cr => cr.Course)
                .HasForeignKey(cr => cr.CourseId);

            modelBuilder.Entity<Trainee>()
                .HasMany(t => t.CourseResults)
                .WithOne(cr => cr.Trainee)
                .HasForeignKey(cr => cr.TraineeId);
        }
    }
}
