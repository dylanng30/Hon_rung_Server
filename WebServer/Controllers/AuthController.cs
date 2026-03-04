using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Requests;
using Shared.DTOs.Response;

namespace WebServer.Controllers;

[Route("api/auth")]
[ApiController]

public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // [LOG] In ra thông tin đăng nhập
        Console.WriteLine($"Data: Email='{request.Email}', Password='{request.Password}'");

        var result = await _authService.Login(request);

        if (!result.Success)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[AuthController] Login Failed: {result.Message}");
            Console.ResetColor();

            return BadRequest(result);
        }

        Console.WriteLine($"[AuthController] Login successfully: {result.Message}");
        return Ok(result);
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupRequest request)
    {
        // [LOG] In ra thông tin đăng ký
        Console.WriteLine($"Data: Registering '{request.Email}'");

        var result = await _authService.Signup(request);

        if (!result.Success)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[AuthController] Signup Failed: {result.Message}");
            Console.ResetColor();

            return BadRequest(result);
        }

        Console.WriteLine($"[AuthController] Signup successfully: {result.Message}");
        return Ok(result);
    }
}
