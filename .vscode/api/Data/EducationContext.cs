using Microsoft.EntityFrameworkCore;
using westcoast_education.api.Models;

namespace westcoast_education.api.Data;

public class EducationContext : DbContext
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<StudentCourse> StudentCourse { get; set;}
    public EducationContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Sätt upp sammansatt primärnyckel som består av CourseId och StudentId...
        modelBuilder.Entity<StudentCourse>()
            .HasKey(sc => new{sc.CourseId, sc.StudentId});
        
        // Sätt upp förhållandet att en student kan vara anmäld på flera kurser...
        modelBuilder.Entity<StudentCourse>()
            .HasOne(sc => sc.Student)
            .WithMany(c => c.StudentCourses)
            .HasForeignKey(sc => sc.StudentId);

        // Sätt upp förhållandet att en kurs kan ha flera studenter...
        modelBuilder.Entity<StudentCourse>()
            .HasOne(sc => sc.Course)
            .WithMany(c => c.StudentCourses)
            .HasForeignKey(sc => sc.CourseId);
    }
}
 