using HealthCommunitiesCheck2.Model;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace HealthCommunitiesCheck2.Data
{
    public class DbSeeder
    {
        private readonly ApplicationDbContext _context;
        //ROLE-ID
        private static readonly Guid AdminRole = Guid.Parse("A1DAB1C3-6D48-4B23-8369-2D1C9C828F22");
        private static readonly Guid StudentRole = Guid.Parse("B2DAB1C3-6D48-4B23-8369-2D1C9C828F22");
        private static readonly Guid LecturerRole = Guid.Parse("C3DAB1C3-6D48-4B23-8369-2D1C9C828F22");
        //USER-ID
        private static readonly Guid AdminID = Guid.Parse("D4DAB1C3-6D48-4B23-8369-2D1C9C828F22");



        public DbSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public static void Seed(ModelBuilder modelBuilder)
        {
            SeedRoles(modelBuilder); // ✅ Gọi SeedRoles() để thêm dữ liệu
            SeedUser(modelBuilder);
        }

        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleID = AdminRole, RoleName = "Admin" },
                new Role { RoleID = LecturerRole, RoleName = "Lecturer" },
                new Role { RoleID = StudentRole, RoleName = "Student" }

            );
        }
        private static void SeedUser(ModelBuilder modelBuilder)
        {
            // Mật khẩu "admin123" đã hash trước, không hash trong code.
            string fixedHashedPassword = "$2a$12$YqP8QbXixw8uHVcnk4JmRO/JQz9jbTko0VhkWylmVs6xqDiZfWpCu";

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserID = AdminID,
                    FullName = "Admin",
                    Email = "admin@example.com",
                    PasswordHash = fixedHashedPassword,
                    Salt = "",
                    RoleID = AdminRole, // ✅ Gán đúng RoleID Admin
                    CreatedAt = DateTime.Parse("2024-02-01T00:00:00Z")
                }
            );
        }

    }
}
