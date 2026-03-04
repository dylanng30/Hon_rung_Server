using System;
using System.Threading;
using GameServer.Physics;

namespace GameServer;

class Program
{
    private static bool isRunning = false;

    static void Main(string[] args)
    {
        Console.Title = "Game Server - Hon rung";
        Initialize();
        CollisionWorld.Initialize();
    }

    private static void Initialize()
    {
        isRunning = true;
        Thread mainThread = new Thread(new ThreadStart(MainThread));
        mainThread.Start();

        Server.Start(50, 26950);
    }

    private static void MainThread()
    {
        Console.WriteLine($"Main thread started. Running at {Constants.TICKS_PER_SEC} ticks per second.");
        DateTime _nextLoop = DateTime.Now;
        float _deltaTime = Constants.MS_PER_TICK / 1000f;

        while (isRunning)
        {
            while (_nextLoop < DateTime.Now)
            {
                GameLogic.Update(_deltaTime);

                _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                if (_nextLoop > DateTime.Now)
                {
                    Thread.Sleep(_nextLoop - DateTime.Now);
                }
            }
        }
    }

    
}