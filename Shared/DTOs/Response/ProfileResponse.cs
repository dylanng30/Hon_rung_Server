using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Response;

public class ProfileResponse
{
    public Guid Id { get; set; }
    public string Nickname { get; set; } = string.Empty;
    public string AvatarUrl { get; set; }
    public int Level { get; set; }
    public int Gold { get; set; }
    public int RankScore { get; set; }
}
