using Application.Interfaces;
using Domain.Interfaces;
using Shared.DTOs.Requests;
using Shared.DTOs.Response;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepo;
    private readonly IProfileRepository _profileRepo;
    private readonly ITokenService _tokenService;

    public AuthService(
        IAuthRepository authRepo, 
        IProfileRepository profileRepo,
        ITokenService tokenService)
    {
        _authRepo = authRepo;
        _profileRepo = profileRepo;
        _tokenService = tokenService;
    }

    public async Task<SignupResponse> Signup(SignupRequest req)
    {
        if (await _authRepo.FindByEmail(req.Email) != null)
            return new SignupResponse 
            { 
                Success = false, 
                Message = "Username đã tồn tại!" 
            };

        //Tách ra sau để dễ quản lý -> PasswordService
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(req.Password); 

        var newAccount = await _authRepo.Create(req.Email, passwordHash);

        await _profileRepo.Create(newAccount.Id.ToString());

        return new SignupResponse
        {
            Success = true,
            Message = "Đăng ký thành công! Vui lòng đăng nhập."
        };
    }

    public async Task<LoginResponse> Login(LoginRequest req)
    {
        var account = await _authRepo.FindByEmail(req.Email);

        if (account == null)
            return new LoginResponse 
            {
                Success = false,
                Message = "Tài khoản không tồn tại!" 
            };

        bool verified = BCrypt.Net.BCrypt.Verify(req.Password, account.PasswordHash);

        if (!verified)
            return new LoginResponse 
            { 
                Success = false, 
                Message = "Mật khẩu không đúng!" 
            };

        // Sau này thay bằng JWT thật -> JWTService
        string accessToken = _tokenService.GenerateAccessToken(account.Id.ToString(), account.Email);
        string refreshToken = _tokenService.GenerateRefreshToken();

        return new LoginResponse
        {
            Success = true,
            Message = "Đăng nhập thành công!",
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccountId = account.Id.ToString()
        };
    }
}
