using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Matchs;

public class Match
{
    public int MatchId;
    public List<int> PlayerIds = new List<int>(); // Danh sách ID người chơi trong phòng này
    public bool IsInProgress = false;

    public Match(int _matchId)
    {
        MatchId = _matchId;
    }

    public void Update(float _deltaTime)
    {
        if (PlayerIds.Count == 0) return;

        // Chỉ update logic cho những người chơi trong phòng này
        foreach (int _clientId in PlayerIds)
        {
            if (Server.clients.ContainsKey(_clientId))
            {
                var player = Server.clients[_clientId].player;
                if (player != null)
                {
                    player.Update(_deltaTime);
                }
            }
        }
    }

    public void AddPlayer(int _clientId)
    {
        if (!PlayerIds.Contains(_clientId))
        {
            PlayerIds.Add(_clientId);
            // Gán MatchId cho Client để biết đang ở phòng nào
            Server.clients[_clientId].CurrentMatchId = MatchId;

            // Gửi thông báo cho người khác trong phòng (Logic Spawn cũ)
            SendToAllInMatch(_clientId);
        }
    }

    public void RemovePlayer(int _clientId)
    {
        if (PlayerIds.Contains(_clientId))
        {
            PlayerIds.Remove(_clientId);
            Server.clients[_clientId].CurrentMatchId = -1;
            // Xử lý logic báo người chơi thoát trận tại đây
        }
    }

    // Hàm gửi dữ liệu chỉ cho người trong phòng (Thay thế ServerSend.SendToAll)
    private void SendToAllInMatch(int _newPlayerId)
    {
        // Logic spawn player cho những người trong cùng Match
        // Bạn sẽ cần sửa lại ServerSend để nhận list Client ID thay vì gửi toàn bộ
    }

}
