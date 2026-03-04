using Shared.DTOs.Requests;
using Shared.DTOs.Response;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<SignupResponse> Signup(SignupRequest req);
    Task<LoginResponse> Login(LoginRequest req);
}
