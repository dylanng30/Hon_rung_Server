using Application.Interfaces;

namespace WebServer.Workers;

public class MatchmakerWorker : BackgroundService
{
    private readonly IMatchmakingService _matchmakingService;
    private readonly ILogger<MatchmakerWorker> _logger;

    // --- CẤU HÌNH LOGIC MATCHMAKING ---
    private const int PLAYERS_PER_MATCH = 2; // Test để 2, thực tế PUBG là 100
    private const int BASE_SCORE_RANGE = 100; // Chênh lệch điểm ban đầu cho phép
    private const int EXPANSION_STEP = 50;    // Cứ 5s nới rộng thêm 50 điểm

    // Giả lập thông tin GameServer (Trong thực tế bạn sẽ quản lý List IP/Port động)
    private const string GAME_SERVER_IP = "127.0.0.1";
    private const int GAME_SERVER_PORT = 26950; // Port UDP/TCP của server game

    public MatchmakerWorker(IMatchmakingService matchmakingService, ILogger<MatchmakerWorker> logger)
    {
        _matchmakingService = matchmakingService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("PUBG Matchmaker Started...");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                ProcessMatchmaking();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in matchmaking loop");
            }

            // Quét 1 giây 1 lần
            await Task.Delay(1000, stoppingToken);
        }
    }

    private void ProcessMatchmaking()
    {
        // 1. Lấy snapshot hàng đợi
        var queue = _matchmakingService.GetQueueSnapshot();
        if (queue.Count < PLAYERS_PER_MATCH) return; // Chưa đủ người tối thiểu

        // 2. Sắp xếp theo thời gian chờ (Người chờ lâu nhất được ưu tiên làm 'Neo')
        var sortedQueue = queue.OrderBy(x => x.JoinTime).ToList();
        var matchedIds = new HashSet<Guid>(); // Đánh dấu người đã được ghép

        foreach (var anchorUser in sortedQueue)
        {
            if (matchedIds.Contains(anchorUser.UserId)) continue;

            // 3. Tính toán độ mở rộng (WaitTime càng lâu -> Range càng lớn)
            double waitSeconds = (DateTime.UtcNow - anchorUser.JoinTime).TotalSeconds;
            int currentRange = BASE_SCORE_RANGE + (int)((waitSeconds / 5.0) * EXPANSION_STEP);

            // 4. Tìm các đồng đội phù hợp
            var potentialMatch = sortedQueue
                .Where(p => !matchedIds.Contains(p.UserId)) // Chưa bị ghép
                .Where(p => Math.Abs(p.Score - anchorUser.Score) <= currentRange) // Điểm trong phạm vi
                .Take(PLAYERS_PER_MATCH) // Lấy đủ số lượng
                .ToList();

            // 5. Nếu tìm đủ người -> Tạo trận
            if (potentialMatch.Count == PLAYERS_PER_MATCH)
            {
                _logger.LogInformation($"[MATCH FOUND] Avg Score: {potentialMatch.Average(x => x.Score)} | Range: {currentRange}");

                // Lấy danh sách ID
                var playerIds = potentialMatch.Select(p => p.UserId).ToList();

                // Tạo trận (Server sẽ lưu Session và xóa User khỏi hàng đợi)
                _matchmakingService.CreateMatch(playerIds, GAME_SERVER_IP, GAME_SERVER_PORT);

                // Đánh dấu để không xét lại trong vòng lặp này
                foreach (var p in potentialMatch) matchedIds.Add(p.UserId);
            }
        }
    }
}