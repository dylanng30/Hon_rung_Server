using System.Text;
using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebServer.Workers;


var builder = WebApplication.CreateBuilder(args);

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("==================================================");
Console.WriteLine($"[START] Đang khởi động WebServer... (Môi trường: {builder.Environment.EnvironmentName})");
Console.WriteLine("==================================================");

// Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("[ERROR] Không tìm thấy chuỗi kết nối 'DefaultConnection'!");
    Console.ResetColor();
}
else
{
    Console.WriteLine($"[INFO] Đã tìm thấy chuỗi kết nối Database.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseSqlServer(connectionString));

//JWT
var jwtKey = builder.Configuration["JwtSettings:Key"];
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false, // Có thể bật true nếu cần
        ValidateAudience = false, // Có thể bật true nếu cần
        ValidateLifetime = true, // Kiểm tra token hết hạn
        ClockSkew = TimeSpan.Zero // Không cho phép lệch giờ (mặc định là 5 phút)
    };
});

// Services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<StorageService>();

builder.Services.AddSingleton<IMatchmakingService, MatchmakingService>();
builder.Services.AddHostedService<MatchmakerWorker>();

// Repositories
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();


// Controller & API Docs
builder.Services.AddControllers();
builder.Services.AddOpenApi();

//Build xong
Console.WriteLine("[INFO] Đang xây dựng ứng dụng (Building)...");
var app = builder.Build();

app.Use(async (context, next) =>
{
    // 1. In ra Request khi vừa nhận được
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"\n[-> REQUEST] {context.Request.Method} {context.Request.Path} (Time: {DateTime.Now:HH:mm:ss})");
    Console.ResetColor();

    await next.Invoke();

    // 2. In ra Response Status sau khi xử lý xong
    if (context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[<- SUCCESS] Status: {context.Response.StatusCode}");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[<- FAILED ] Status: {context.Response.StatusCode}");
    }
    Console.ResetColor();
});


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// 4. Sự kiện khi Server chạy thành công
app.Lifetime.ApplicationStarted.Register(() => {
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\n==================================================");
    Console.WriteLine("[SUCCESS] WebServer ĐÃ CHẠY THÀNH CÔNG!");
    Console.WriteLine("--------------------------------------------------");
    Console.WriteLine($"[*] Database: SQL Server");
    Console.WriteLine($"[*] OpenAPI : {app.Environment.WebRootPath}/openapi/v1.json (Mặc định)");
    Console.WriteLine("==================================================\n");
    Console.ResetColor();
});

// Chạy ứng dụng
app.Run();