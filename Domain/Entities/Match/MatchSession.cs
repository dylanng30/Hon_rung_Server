using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Match;

public class MatchSession
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public List<Guid> PlayerIds { get; set; } = new();
    public string ServerIp { get; set; }
    public int ServerPort { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
