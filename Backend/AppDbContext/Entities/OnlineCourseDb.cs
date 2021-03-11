using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AppDbContext.Entities
{
    public partial class OnlineCourseDb : IdentityDbContext<ApplicationUser>
    {
        public OnlineCourseDb()
            : base("name=OnlineCourseDb")
        {
        }

        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<Availability> Availabilities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentLang> StudentLangs { get; set; }
        public virtual DbSet<StudentSubj> StudentSubjs { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<TeacherAvailability> TeacherAvailabilities { get; set; }
        public virtual DbSet<TeacherLang> TeacherLangs { get; set; }
        public virtual DbSet<TeacherLevel> TeacherLevels { get; set; }
        public virtual DbSet<TeacherSubj> TeacherSubjs { get; set; }

        public static OnlineCourseDb Create()
        {
            return new OnlineCourseDb();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Availability>()
                .HasMany(e => e.Sessions)
                .WithRequired(e => e.Availability)
                .HasForeignKey(e => e.AvailabilityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Availability>()
                .HasMany(e => e.TeacherAvailabilities)
                .WithRequired(e => e.Availability)
                .HasForeignKey(e => e.AvailabilityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teacher>()
                .HasRequired(c => c.User)
                .WithRequiredDependent(c => c.Teacher);

            modelBuilder.Entity<Student>()
                .HasRequired(c => c.User)
                .WithRequiredDependent(c => c.Student);

            
            modelBuilder.Entity<Availability>()
                .HasMany(e => e.TeacherAvailabilities)
                .WithRequired(e => e.Availability)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Students)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Teachers)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Language>()
                .HasMany(e => e.Sessions)
                .WithRequired(e => e.Language)
                .HasForeignKey(e => e.LangId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Language>()
                .HasMany(e => e.TeacherLangs)
                .WithRequired(e => e.Language)
                .HasForeignKey(e => e.LangId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Level>()
                .HasMany(e => e.Sessions)
                .WithRequired(e => e.Level)
                .HasForeignKey(e => e.LevelId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Level>()
                .HasMany(e => e.TeacherLevels)
                .WithRequired(e => e.Level)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Session>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<Session>()
                .HasMany(e => e.Assignments)
                .WithRequired(e => e.Session)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Sessions)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Sessions)
                .WithRequired(e => e.Subject)
                .HasForeignKey(e => e.SubjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.TeacherSubjs)
                .WithRequired(e => e.Subject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.Sessions)
                .WithRequired(e => e.Teacher)
                .HasForeignKey(e => e.TeacherId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.TeacherAvailabilities)
                .WithRequired(e => e.Teacher)
                .HasForeignKey(c => c.Teacher_UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.TeacherLangs)
                .WithRequired(e => e.Teacher)
                .HasForeignKey(c => c.Teacher_UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.TeacherLevels)
                .WithRequired(e => e.Teacher)
                .HasForeignKey(c => c.Teacher_UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.TeacherSubjs)
                .WithRequired(e => e.Teacher)
                .HasForeignKey(c => c.Teacher_UserId)
                .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }
    }
}
