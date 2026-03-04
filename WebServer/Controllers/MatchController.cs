using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Requests;
using System.Security.Claims;

namespace WebServer.Controllers;

[Route("api/matchmaking")]
[ApiController]
[Authorize]
public class MatchController : ControllerBase
{
    private readonly IMatchmakingService _matchmakingService;

    public MatchController(IMatchmakingService matchmakingService)
    {
        _matchmakingService = matchmakingService;
    }

    [HttpPost("find")]
    public IActionResult FindMatch([FromBody] MatchFindRequest request)
    {
        // Lấy UserId từ Token JWT
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Console.WriteLine($"User {userIdStr} tìm trận");
        if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();
        var userId = Guid.Parse(userIdStr);

        // Thêm vào hàng chờ
        //_matchmakingService.AddToQueue(userId, request.Score);

        return Ok(new { message = "Queued", status = "Searching" });
    }

    // GET: api/matchmaking/status
    [HttpGet("status")]
    public IActionResult CheckStatus()
    {
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();
        var userId = Guid.Parse(userIdStr);

        var match = _matchmakingService.GetMatchStatus(userId);

        if (match != null)
        {
            return Ok(new
            {
                status = "Found",
                matchId = match.Id,
                ip = match.ServerIp,
                port = match.ServerPort
            });
        }

        return Ok(new { status = "Searching" });
    }

    // POST: api/matchmaking/cancel
    [HttpPost("cancel")]
    public IActionResult CancelMatch()
    {
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();
        var userId = Guid.Parse(userIdStr);

        _matchmakingService.RemoveFromQueue(userId);

        return Ok(new { message = "Canceled", status = "Idle" });
    }
}