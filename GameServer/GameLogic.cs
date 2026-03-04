using GameServer.Matchs;

namespace GameServer;

class GameLogic
{
    public static void Update(float deltaTime)
    {
        MatchManager.Update(deltaTime);

        foreach (Client _client in Server.clients.Values)
        {
            if (_client.player != null)
            {
                _client.player.Update(deltaTime);
            }
        }

        ThreadManager.UpdateMain();
    }
}