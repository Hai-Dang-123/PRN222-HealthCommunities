using HealthCommunitiesCheck2.DTO;
using HealthCommunitiesCheck2.UnitOfWork;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

using BCrypt;
using HealthCommunitiesCheck2.Model;
using HealthCommunitiesCheck2.IService;

namespace HealthCommunitiesCheck2.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterService (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

      

        public async Task<ResponseDTO> Register(RegisterDTO register)
        {
            if (string.IsNullOrWhiteSpace(register.FullName))
            {
                return new ResponseDTO("FullName cannot be blank.", 400, false);
            }
            if (string.IsNullOrWhiteSpace(register.Email))
            {
                return new ResponseDTO("Email cannot be blank.", 400, false);
            }

            if (string.IsNullOrWhiteSpace(register.Password))
            {
                return new ResponseDTO("Password cannot be blank.", 400, false);
            }

            if (register.Password != register.PasswordConfirmed)
            {
                return new ResponseDTO("Passwords do not match.", 400, false);
            }

            try
            {
                var existingUser = await _unitOfWork.User.FindByEmailAsync(register.Email);
                if (existingUser != null)
                {
                    return new ResponseDTO("Email already exists.", 400, false);
                }

                // ✅ Tìm RoleID từ RoleName
                var role = await _unitOfWork.Role.FirstOrDefaultAsync(r => r.RoleName == register.RoleName);
                if (role == null)
                {
                    return new ResponseDTO("Invalid Role.", 400, false);
                }

                // Hash password
                string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(register.Password, salt);

                // Tạo user mới
                var newUser = new User
                {
                    UserID = Guid.NewGuid(),
                    FullName = register.FullName,
                    Email = register.Email,
                    PasswordHash = hashedPassword,
                    Salt = salt,
                    RoleID = role.RoleID, // Dùng Guid RoleID từ View
                    CreatedAt = DateTime.UtcNow
                };

                // Lưu vào DB
                await _unitOfWork.User.AddAsync(newUser);

                // Tạo wallet
                var newWallet = new Wallet
                {
                    WalletID = Guid.NewGuid(),
                    UserID = newUser.UserID,
                    Balance = 0, // Ví khởi tạo với số dư 0
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                // Thêm wallet vào DB
                await _unitOfWork.Wallet.AddAsync(newWallet);
                // lưu tổng
                await _unitOfWork.SaveAsync();

                return new ResponseDTO("User registered successfully.", 200, true);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error: {ex.Message}", 500, false);
            }

        }
    }
}
