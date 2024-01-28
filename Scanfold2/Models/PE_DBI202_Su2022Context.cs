using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Scanfold2.Models
{
    public partial class PE_DBI202_Su2022Context : DbContext
    {
        public PE_DBI202_Su2022Context()
        {
        }

        public PE_DBI202_Su2022Context(DbContextOptions<PE_DBI202_Su2022Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public virtual DbSet<OnlineCourse> OnlineCourses { get; set; }
        public virtual DbSet<OnsiteCourse> OnsiteCourses { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<StudentGrade> StudentGrades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=127.0.0.1,1433;Initial Catalog=PE_DBI202_Su2022;User ID=SA;Password=123456aA@;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CourseId)
                    .ValueGeneratedNever()
                    .HasColumnName("CourseID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_Department");

                entity.HasMany(d => d.People)
                    .WithMany(p => p.Courses)
                    .UsingEntity<Dictionary<string, object>>(
                        "CourseInstructor",
                        l => l.HasOne<Person>().WithMany().HasForeignKey("PersonId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_CourseInstructor_Person"),
                        r => r.HasOne<Course>().WithMany().HasForeignKey("CourseId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_CourseInstructor_Course"),
                        j =>
                        {
                            j.HasKey("CourseId", "PersonId").HasName("PK__CourseIn__838F8E3F01D38872");

                            j.ToTable("CourseInstructor");

                            j.IndexerProperty<int>("CourseId").HasColumnName("CourseID");

                            j.IndexerProperty<int>("PersonId").HasColumnName("PersonID");
                        });
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentId)
                    .ValueGeneratedNever()
                    .HasColumnName("DepartmentID");

                entity.Property(e => e.Budget).HasColumnType("money");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OfficeAssignment>(entity =>
            {
                entity.HasKey(e => e.InstructorId)
                    .HasName("PK__OfficeAs__9D010B7B4EF783D9");

                entity.ToTable("OfficeAssignment");

                entity.Property(e => e.InstructorId)
                    .ValueGeneratedNever()
                    .HasColumnName("InstructorID");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Instructor)
                    .WithOne(p => p.OfficeAssignment)
                    .HasForeignKey<OfficeAssignment>(d => d.InstructorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OfficeAssignment_Person");
            });

            modelBuilder.Entity<OnlineCourse>(entity =>
            {
                entity.HasKey(e => e.CourseId)
                    .HasName("PK__OnlineCo__C92D7187EB93B62E");

                entity.ToTable("OnlineCourse");

                entity.Property(e => e.CourseId)
                    .ValueGeneratedNever()
                    .HasColumnName("CourseID");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("URL");

                entity.HasOne(d => d.Course)
                    .WithOne(p => p.OnlineCourse)
                    .HasForeignKey<OnlineCourse>(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OnlineCourse_Course");
            });

            modelBuilder.Entity<OnsiteCourse>(entity =>
            {
                entity.HasKey(e => e.CourseId)
                    .HasName("PK__OnsiteCo__C92D7187285EEE4C");

                entity.ToTable("OnsiteCourse");

                entity.Property(e => e.CourseId)
                    .ValueGeneratedNever()
                    .HasColumnName("CourseID");

                entity.Property(e => e.Days)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Time).HasColumnType("smalldatetime");

                entity.HasOne(d => d.Course)
                    .WithOne(p => p.OnsiteCourse)
                    .HasForeignKey<OnsiteCourse>(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OnsiteCourse_Course");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.PersonId)
                    .ValueGeneratedNever()
                    .HasColumnName("PersonID");

                entity.Property(e => e.Discriminator)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EnrollmentDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HireDate).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StudentGrade>(entity =>
            {
                entity.HasKey(e => e.EnrollmentId)
                    .HasName("PK__StudentG__7F6877FBE0D60AB1");

                entity.ToTable("StudentGrade");

                entity.Property(e => e.EnrollmentId)
                    .ValueGeneratedNever()
                    .HasColumnName("EnrollmentID");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.Grade).HasColumnType("decimal(3, 2)");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentGrades)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentGrade_Course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentGrades)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentGrade_Student");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
