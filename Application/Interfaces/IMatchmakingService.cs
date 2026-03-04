using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Match;

namespace Application.Interfaces;

public interface IMatchmakingService
{
    //CONTROLLER
    void AddToQueue(Guid userId, int scoreBlance); //Thêm người vào hàng chờ
    void RemoveFromQueue(Guid userId); //Hủy tìm trận (Xóa khỏi hàng chờ)
    MatchSession? GetMatchStatus(Guid userId); //Kiểm tra user có đang trong trận đấu đã tạo xong chưa

    //WORKER
    List<MatchQueue> GetQueueSnapshot(); //Lấy bản sao danh sách đang chờ để Worker xử lý (Thread-safe)
    void CreateMatch(List<Guid> playerIds, string ip, int port);//Tạo trận đấu thành công -> Xóa user khỏi Queue + Tạo Session
}
