using Microsoft.EntityFrameworkCore;
using DbExample.Data.Entities;

namespace DbExample.Data
{
    public class AtpdbContext : DbContext
    {
        public AtpdbContext(DbContextOptions<AtpdbContext> options)
            : base(options)
        {
        }

        //property for each table in the database
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Programs> Programs { get; set; }
        public DbSet<FacultyMember> FacultyMember { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<OfferedCourse> OfferedCourses { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<RegistrationCourse> RegistrationCourses { get; set; }
        public DbSet<StudentPayment> StudentPayments { get; set; }
        public DbSet<SemesterBill> SemesterBills { get; set; }
        public DbSet<CourseCatalog> CourseCatalogs { get; set; }

        // Configure primary keys and relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Faculty>()
                .ToTable("Faculty");

            modelBuilder.Entity<Programs>(entity =>
            {
                entity.ToTable("Programs");
                entity.HasKey(e => e.ProgramId);
                entity.Property(e => e.ProgramName).HasMaxLength(150);
                entity.Property(e => e.TotalCredit).HasColumnName("TotalCredit").HasColumnType("decimal(18,2)");
                entity.Property(e => e.DurationYears).HasColumnType("decimal(18,2)");

                entity.HasData(
                    new Programs { ProgramId = 1, DepartmentId = 4, ProgramName = "BSc in Computer Science & Engineering (CSE)", TotalCredit = 148.00m, DurationYears = 4.0m },
                    new Programs { ProgramId = 2, DepartmentId = 4, ProgramName = "BSc in Software Engineering (SE)", TotalCredit = 148.00m, DurationYears = 4.0m },
                    new Programs { ProgramId = 3, DepartmentId = 3, ProgramName = "BSc in Electrical & Electronic Engineering (EEE)", TotalCredit = 148.00m, DurationYears = 4.0m },
                    new Programs { ProgramId = 4, DepartmentId = 2, ProgramName = "Bachelor of Business Administration (BBA)", TotalCredit = 130.00m, DurationYears = 4.0m },
                    new Programs { ProgramId = 5, DepartmentId = 1, ProgramName = "Bachelor of Arts in English (BA English)", TotalCredit = 120.00m, DurationYears = 4.0m }
                );
            });
        }
    }
}

