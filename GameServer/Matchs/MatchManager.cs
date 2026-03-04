using System.Collections.Generic;

namespace GameServer.Matchs;

public class MatchManager
{
    public static Dictionary<int, Match> Matches = new Dictionary<int, Match>();
    private static int nextMatchId = 1;

    public static void Initialize()
    {
        CreateMatch();
        CreateMatch();
        Console.WriteLine("MatchManager Initialized with 2 active matches.");
    }

    public static Match CreateMatch()
    {
        Match newMatch = new Match(nextMatchId);
        Matches.Add(nextMatchId, newMatch);
        nextMatchId++;
        return newMatch;
    }

    public static void Update(float _deltaTime)
    {
        foreach (var match in Matches.Values)
        {
            match.Update(_deltaTime);
        }
    }

    //TEMP
    public static void JoinAvailableMatch(int _clientId)
    {
        //Ưu tiên phòng còn trống
        foreach (var match in Matches.Values)
        {
            if (match.PlayerIds.Count < 20) //Max 20 người 
            {
                match.AddPlayer(_clientId);
                Console.WriteLine($"Client {_clientId} joined Match {match.MatchId}");
                return;
            }
        }

        //Nếu full hết thì tạo phòng mới
        Match newMatch = CreateMatch();
        newMatch.AddPlayer(_clientId);
        Console.WriteLine($"Client {_clientId} created and joined Match {newMatch.MatchId}");
    }
}