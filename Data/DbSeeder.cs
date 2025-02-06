using HealthCommunitiesCheck2.Model;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace HealthCommunitiesCheck2.Data
{
    public class DbSeeder
    {
        private readonly ApplicationDbContext _context;

        public DbSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            //    // Kiểm tra xem cơ sở dữ liệu đã có dữ liệu chưa để tránh seeding lại khi ứng dụng khởi động lại
            //    if (!_context.Roles.Any())
            //    {
            //        SeedRoles();
            //    }

            //    if (!_context.Users.Any())
            //    {
            //        SeedUsers();
            //    }

            //    if (!_context.Courses.Any())
            //    {
            //        SeedCourses();
            //    }

            //    if (!_context.Enrollments.Any())
            //    {
            //        SeedEnrollments();
            //    }

            //    if (!_context.News.Any())
            //    {
            //        SeedNews();
            //    }

            //    _context.SaveChanges();
            //}

            //private void SeedRoles()
            //{
            //    _context.Roles.AddRange(
            //        new Role { RoleID = 1, RoleName = "Admin" },
            //        new Role { RoleID = 2, RoleName = "Lecturer" },
            //        new Role { RoleID = 3, RoleName = "Student" },
            //        new Role { RoleID = 4, RoleName = "Guest" }
            //    );
            //}

            //private void SeedUsers()
            //{
            //    var salt = new byte[128 / 8];
            //    using (var rng = new RNGCryptoServiceProvider())
            //    {
            //        rng.GetBytes(salt);
            //    }

            //    string passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            //        password: "adminpassword",
            //        salt: salt,
            //        prf: KeyDerivationPrf.HMACSHA512,
            //        iterationCount: 10000,
            //        numBytesRequested: 256 / 8));

            //    _context.Users.AddRange(
            //        new User
            //        {
            //            UserID = 1,
            //            FullName = "Admin User",
            //            Email = "admin@example.com",
            //            PasswordHash = passwordHash,
            //            Salt = Convert.ToBase64String(salt),
            //            RoleID = 1,
            //            CreatedAt = DateTime.Now
            //        },
            //        new User
            //        {
            //            UserID = 2,
            //            FullName = "Lecturer User",
            //            Email = "lecturer@example.com",
            //            PasswordHash = passwordHash,
            //            Salt = Convert.ToBase64String(salt),
            //            RoleID = 2,
            //            CreatedAt = DateTime.Now
            //        },
            //        new User
            //        {
            //            UserID = 3,
            //            FullName = "Student User",
            //            Email = "student@example.com",
            //            PasswordHash = passwordHash,
            //            Salt = Convert.ToBase64String(salt),
            //            RoleID = 3,
            //            CreatedAt = DateTime.Now
            //        }
            //    );
            //}

            //private void SeedCourses()
            //{
            //    _context.Courses.AddRange(
            //        new Course
            //        {
            //            CourseID = 1,
            //            Title = "C# Programming",
            //            Description = "Learn C# programming from scratch.",
            //            StartDate = DateTime.Now.AddDays(1),
            //            EndDate = DateTime.Now.AddMonths(2),
            //            UserID = 2,
            //            IsOnline = true,
            //            Price = 199.99m,
            //            CreatedAt = DateTime.Now
            //        },
            //        new Course
            //        {
            //            CourseID = 2,
            //            Title = "Web Development",
            //            Description = "Learn web development with HTML, CSS, and JavaScript.",
            //            StartDate = DateTime.Now.AddDays(1),
            //            EndDate = DateTime.Now.AddMonths(3),
            //            UserID = 2,
            //            IsOnline = false,
            //            Price = 299.99m,
            //            CreatedAt = DateTime.Now
            //        }
            //    );
            //}

            //private void SeedEnrollments()
            //{
            //    _context.Enrollments.AddRange(
            //        new Enrollment
            //        {
            //            EnrollmentID = 1,
            //            UserID = 3,
            //            CourseID = 1,
            //            EnrollmentDate = DateTime.Now,
            //            Status = "Enrolled"
            //        }
            //    );
            //}

            //private void SeedNews()
            //{
            //    _context.News.AddRange(
            //        new News
            //        {
            //            NewsID = 1,
            //            Title = "New Course Available!",
            //            Content = "We have launched a new course on C# programming. Sign up now!",
            //            CreatedAt = DateTime.Now
            //        }
            //    );
        }
    }

}
