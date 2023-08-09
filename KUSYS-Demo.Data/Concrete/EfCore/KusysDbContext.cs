using System;
using Microsoft.EntityFrameworkCore;
using KUSYS_Demo.Entity;

namespace KUSYS_Demo.Data.Concrete.EfCore
{
	public class KusysDbContext : DbContext
    {
		public KusysDbContext(DbContextOptions<KusysDbContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var course1 = new Course
            {
                CourseId = "CSI101",
                CourseName = "Introduction to Computer Science"
            };
            var course2 = new Course
            {
                CourseId = "CSI102",
                CourseName = "Algorithms"
            };
            var course3 = new Course
            {
                CourseId = "MAT101",
                CourseName = "Calculus"
            };
            var course4 = new Course
            {
                CourseId = "PHY101",
                CourseName = "Physics"
            };
            modelBuilder.Entity<Course>()
                .HasKey(u => u.CourseId);
            modelBuilder.Entity<Course>()
                .HasData(course1, course2, course3, course4);

            var admin = new Users
            {
                EMail = "hakandulger91@gmail.com",
                UserName = "admin",
                FirstName = "Hakan",
                LastName = "Dulger",
                IdentityId = Guid.Parse("04c5efcb-fb2a-4676-ad47-17171bf52945"),
                Password = "Ha_140791",
                UserId = 1,
            };

            var user = new Users
            {
                EMail = "hakandulger@outlook.com",
                UserName = "hakan",
                FirstName = "Hakan",
                LastName = "Dulger",
                IdentityId = Guid.Parse("ae6e7788-8e4a-470d-92e1-8858b20ecd35"),
                Password = "Ha_123987",
                UserId = 2,
            };
            modelBuilder.Entity<Users>()
                .HasKey(u => u.UserId);
            modelBuilder.Entity<Users>()
                .HasData(admin, user);

            //modelBuilder.Entity<Users>()
            //.HasIndex(b => b.UserId);
            //modelBuilder.Entity<Student>()
            //.HasIndex(b => b.StudentId);
            //modelBuilder.Entity<Course>()
            //.HasIndex(b => b.CourseId);
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Matchings> Matchings { get; set; }

    }
}

