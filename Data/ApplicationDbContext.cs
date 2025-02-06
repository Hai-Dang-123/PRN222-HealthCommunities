using HealthCommunitiesCheck2.Model;
using Microsoft.EntityFrameworkCore;

namespace HealthCommunitiesCheck2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ReadingOfCourse> Readings { get; set; }
        public DbSet<VideoOfCourse> Videos { get; set; }
        public DbSet<News> News { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Khóa chính cho các entity
            modelBuilder.Entity<User>().HasKey(u => u.UserID);
            modelBuilder.Entity<Course>().HasKey(c => c.CourseID);
            modelBuilder.Entity<Enrollment>().HasKey(e => e.EnrollmentID);
            modelBuilder.Entity<Contact>().HasKey(c => c.ContactID);
            modelBuilder.Entity<ReadingOfCourse>().HasKey(r => r.ReadingID);
            modelBuilder.Entity<VideoOfCourse>().HasKey(v => v.VideoID);
            modelBuilder.Entity<News>().HasKey(n => n.NewsID);

            // Định nghĩa quan hệ giữa các bảng
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleID);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.User)
                .WithMany(u => u.Courses)
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReadingOfCourse>()
                .HasOne(r => r.Course)
                .WithMany(c => c.Readings)
                .HasForeignKey(r => r.CourseID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VideoOfCourse>()
                .HasOne(v => v.Course)
                .WithMany(c => c.Videos)
                .HasForeignKey(v => v.CourseID)
                .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình cột Price để tránh lỗi
            modelBuilder.Entity<Course>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)");

            // Thêm index để tối ưu truy vấn
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
