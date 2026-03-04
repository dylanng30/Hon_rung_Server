using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities.Match;

namespace Application.Services;

public class MatchmakingService : IMatchmakingService
{
    // Dùng List + Lock để dễ dàng xóa phần tử giữa danh sách (khi user Cancel)
    private static readonly List<MatchQueue> _queue = new();
    private static readonly object _queueLock = new(); // Khóa để bảo vệ List

    // Dictionary lưu trận đấu đã tạo xong
    private static readonly ConcurrentDictionary<Guid, MatchSession> _activeMatches = new();

    public void AddToQueue(Guid userId, int score)
    {
        lock (_queueLock)
        {
            _queue.RemoveAll(x => x.UserId == userId);

            _queue.Add(new MatchQueue
            {
                UserId = userId,
                Score = score,
                JoinTime = DateTime.UtcNow
            });

            _activeMatches.TryRemove(userId, out _);
        }
    }

    public void RemoveFromQueue(Guid userId)
    {
        lock (_queueLock)
        {
            _queue.RemoveAll(x => x.UserId == userId);
        }

        _activeMatches.TryRemove(userId, out _);
    }

    public MatchSession? GetMatchStatus(Guid userId)
    {
        if (_activeMatches.TryGetValue(userId, out var session))
        {
            return session;
        }
        return null;
    }

    public List<MatchQueue> GetQueueSnapshot()
    {
        lock (_queueLock)
        {
            return _queue.ToList();
        }
    }

    public void CreateMatch(List<Guid> playerIds, string ip, int port)
    {
        var session = new MatchSession
        {
            Id = Guid.NewGuid(),
            ServerIp = ip,
            ServerPort = port,
            PlayerIds = playerIds,
            Status = "Found"
        };

        lock (_queueLock)
        {
            foreach (var uid in playerIds)
            {
                _activeMatches.TryAdd(uid, session);
                _queue.RemoveAll(x => x.UserId == uid);
            }
        }
    }

}
