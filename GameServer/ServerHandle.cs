using System.Numerics;
using GameServer.Matchs;
using Shared;

namespace GameServer;

class ServerHandle
{
    public static async void WelcomeReceived(int _fromClient, Packet _packet)
    {
        int _clientIdCheck = _packet.ReadInt();
        //string _username = _packet.ReadString();

        Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");

        Server.clients[_fromClient].SendIntoGame();
        //MatchManager.JoinAvailableMatch(_fromClient); // Ném vào phòng
    }

    public static void PlayerInput(int _fromClient, Packet _packet)
    {
        bool[] _inputs = new bool[_packet.ReadInt()];
        for (int i = 0; i < _inputs.Length; i++)
        {
            _inputs[i] = _packet.ReadBool();
        }
        Quaternion _camRotation = _packet.ReadQuaternion();

        Server.clients[_fromClient].player.SetInput(_inputs, _camRotation);
    }

    public static void PlayerOnAnimationEnterEvent(int _fromClient, Packet _packet)
    {
        Server.clients[_fromClient].player.OnAnimationEnterEvent();
    }
    public static void PlayerOnAnimationTransitionEvent(int _fromClient, Packet _packet)
    {
        Server.clients[_fromClient].player.OnAnimationTransitionEvent();
    }
    public static void PlayerOnAnimationExitEvent(int _fromClient, Packet _packet)
    {
        Server.clients[_fromClient].player.OnAnimationExitEvent();
    }


}