using Microsoft.EntityFrameworkCore;
using net_5_api_with_authentication.Models;
using System;

namespace net_5_api_with_authentication.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Course>().HasData(
                new { CourseID = 1, Title = "Programação Orientada a Objetos", Credits = 44 },
                new { CourseID = 2, Title = "Engenharia de Software", Credits = 33 },
                new { CourseID = 3, Title = "Introdução a Algoritmos", Credits = 25 },
                new { CourseID = 4, Title = "Desenvolvimento Web", Credits = 15 }
                );
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Student>().HasData(

                new { ID = 1, LastName = "Guimarães", FirstMidName = "Pinto", EnrollmentDate = DateTime.Now },
                new { ID = 2, LastName = "Student", FirstMidName = "One", EnrollmentDate = DateTime.Now },
                new { ID = 3, LastName = "Dedicated", FirstMidName = "Student", EnrollmentDate = DateTime.Now },
                new { ID = 4, LastName = "Pedro", FirstMidName = "Fernandes", EnrollmentDate = DateTime.Now },
                new { ID = 5, LastName = "Daniel", FirstMidName = "Anderle", EnrollmentDate = DateTime.Now }
                );
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Enrollment>().HasData(
                new { EnrollmentID = 1, CourseID = 1, StudentID = 1, Grade = Grade.B},
                new { EnrollmentID = 2, CourseID = 2, StudentID = 2, Grade = Grade.A},
                new { EnrollmentID = 3, CourseID = 3, StudentID = 2, Grade = Grade.C},
                new { EnrollmentID = 4, CourseID = 3, StudentID= 3, Grade = Grade.F},
                new { EnrollmentID = 5, CourseID = 4, StudentID = 4, Grade = Grade.D}
                );
        }
    }
}