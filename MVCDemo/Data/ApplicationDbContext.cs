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
            // Department Relationships
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Courses)
                .WithOne(c => c.Department)
                .HasForeignKey(c => c.DeptId);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Instructors)
                .WithOne(i => i.Department)
                .HasForeignKey(i => i.DeptId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete on DeptId

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Trainees)
                .WithOne(t => t.Department)
                .HasForeignKey(t => t.DeptId);

            // Course Relationships
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Instructors)
                .WithOne(i => i.Course)
                .HasForeignKey(i => i.CourseId)
                .OnDelete(DeleteBehavior.Cascade); // Keep cascading delete on CourseId

            modelBuilder.Entity<Course>()
                .HasMany(c => c.CourseResults)
                .WithOne(cr => cr.Course)
                .HasForeignKey(cr => cr.CourseId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete on CourseResults

            // Trainee Relationships
            modelBuilder.Entity<Trainee>()
                .HasMany(t => t.CourseResults)
                .WithOne(cr => cr.Trainee)
                .HasForeignKey(cr => cr.TraineeId)
                .OnDelete(DeleteBehavior.Cascade); // Keep cascading delete on TraineeId
        }


    }
}
