using HealthCommunitiesCheck2.Auth;
using HealthCommunitiesCheck2.Data;
using HealthCommunitiesCheck2.IService;
using HealthCommunitiesCheck2.Services;
using HealthCommunitiesCheck2.UnitOfWork;
using HealthCommunitiesCheck2.Utilities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Thêm Session vào DI container
//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30); // Timeout 30 phút
//    options.Cookie.HttpOnly = true;  // Bảo mật chống XSS
//    options.Cookie.IsEssential = true;
//});

// Thêm IHttpContextAccessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.
builder.Services.AddRazorPages();

// Đăng ký IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Dependency Injection cho các dịch vụ

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<UserUtility>();



// Đăng ký DbContext vào DI container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký DbSeeder vào DI container
builder.Services.AddScoped<DbSeeder>();

var app = builder.Build();

// Kiểm tra môi trường và xử lý exception nếu không phải là môi trường phát triển
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




app.UseHttpsRedirection();
app.UseStaticFiles();

//app.UseSession();

app.UseRouting();


app.UseAuthorization();

app.MapRazorPages();

// 🔹 Thêm Seeder khi ứng dụng khởi động

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate(); // Đảm bảo database được cập nhật
}

app.Run();
