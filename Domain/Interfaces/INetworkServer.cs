using Shared;

namespace Domain.Interfaces;

public interface INetworkServer
{
    void Start(int port);
    void Stop();

    //reliable = true -> TCP
    //rebiable = false -> UDP
    void SendToClient(int connectionId, Packet packet, bool reliable);
    void SendToAll(Packet packet, bool reliable);

    // Sự kiện để tầng Application lắng nghe
    event Action<int> OnClientConnected;
    event Action<int> OnClientDisconnected;
    event Action<int, Packet> OnDataReceived; // int: ConnectionId
}
