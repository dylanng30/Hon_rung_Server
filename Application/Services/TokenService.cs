using Application.Interfaces;
using Microsoft.Extensions.Configuration; // Cần thiết để đọc config
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateAccessToken(string userId, string email)
    {
        // Lấy Key từ appsettings.json
        var jwtKey = _configuration["JwtSettings:Key"];
        if (string.IsNullOrEmpty(jwtKey))
            throw new Exception("Chưa cấu hình JwtSettings:Key trong appsettings.json");

        var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Email, email),
            // Thêm các claim khác nếu cần (ví dụ Role)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7), // Token hết hạn sau 7 ngày
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        // Tạo một chuỗi ngẫu nhiên làm Refresh Token
        return Guid.NewGuid().ToString();
    }
}