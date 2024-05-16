using Microsoft.EntityFrameworkCore;
using PRN231_AS1_API.Models;

namespace PRN231_AS1_API
{
    public class ApplicationDBContext :DbContext
    {
          public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student>  Students { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; } 
        public DbSet<Course> Courses { get; set; }
        public DbSet<LecturerCourse> LecturerCourses { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-to-Many: LecturerCourse
            modelBuilder.Entity<LecturerCourse>()
                .HasKey(lc => new { lc.LecturerId, lc.CourseId });

            modelBuilder.Entity<LecturerCourse>()
                .HasOne(lc => lc.Lecturer)
                .WithMany(l => l.LecturerCourses)
                .HasForeignKey(lc => lc.LecturerId);

            modelBuilder.Entity<LecturerCourse>()
                .HasOne(lc => lc.Course)
                .WithMany(c => c.LecturerCourses)
                .HasForeignKey(lc => lc.CourseId);

            // One-to-Many: Grade
            modelBuilder.Entity<Grade>()
                .HasKey(g => new { g.StudentId, g.CourseId });

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentId);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Course)
                .WithMany(c => c.Grades)
                .HasForeignKey(g => g.CourseId);

        }
    }
}
