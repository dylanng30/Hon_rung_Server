using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Match;

public class MatchQueue
{
    public Guid UserId { get; set; }
    public DateTime JoinTime { get; set; }
    public int Score { get; set; }
}
