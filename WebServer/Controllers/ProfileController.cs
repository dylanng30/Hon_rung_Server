using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers;

[Route("api/profile")]
[ApiController]

public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetMyProfile()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
        {
            return Unauthorized(new { message = "Token không hợp lệ hoặc thiếu thông tin ID." });
        }

        Console.WriteLine($"Data: Fetching ME Profile ID = {userId}");

        var profile = await _profileService.GetProfileById(userId);

        if (profile == null)
        {
            return NotFound(new { message = "Không tìm thấy hồ sơ." });
        }

        return Ok(profile);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProfile(Guid id)
    {
        Console.WriteLine($"Data: Fetching Profile ID = {id}");
        var profile = await _profileService.GetProfileById(id);

        if (profile == null)
        {
            Console.WriteLine("   ! Warning: Profile not found in DB.");
            return NotFound(new { message = "Không tìm thấy hồ sơ người dùng!" });
        }

        return Ok(profile);
    }

    /*[HttpPost("{id}/avatar")]
    public async Task<IActionResult> UploadAvatar(Guid id, IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(new { message = "Vui lòng chọn file ảnh." });
        }

        try
        {
            Console.WriteLine($"   + Upload: Receiving avatar for User ID = {id}");

            using var stream = file.OpenReadStream();
            var avatarUrl = await _profileService.UploadAvatarAsync(
                id,
                stream,
                file.FileName,
                file.ContentType
            );

            return Ok(new { message = "Upload thành công!", url = avatarUrl });
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"   [ERROR] Upload failed: {ex.Message}");
            Console.ResetColor();
            return StatusCode(500, new { message = "Lỗi máy chủ nội bộ", error = ex.Message });
        }
    }*/
}