using System.Numerics;

namespace GameServer;

class Client
{
    public int id;
    public TCP tcp;
    public UDP udp;

    public Player player;


    public int CurrentMatchId = -1;

    public Client(int _clientId)
    {
        id = _clientId;
        tcp = new TCP(id);
        udp = new UDP(id);
    }

    public void SendIntoGame()
    {
        //player = new Player(id, Profile.Nickname, new Vector3(0, 0, 0));
        player = new Player(id, "Nickname", new Vector3(0, 0, 0));

        foreach (Client _client in Server.clients.Values)
        {
            if (_client.player != null)
            {
                if (_client.id != id)
                {
                    ServerSend.SpawnPlayer(id, _client.player);
                }
            }
        }

        foreach (Client _client in Server.clients.Values)
        {
            if (_client.player != null)
            {
                ServerSend.SpawnPlayer(_client.id, player);
            }
        }
    }

    public void Disconnect()
    {
        Console.WriteLine($"{tcp.socket.Client.RemoteEndPoint} has disconnected.");

        player = null;

        tcp.Disconnect();
        udp.Disconnect();
    }

}