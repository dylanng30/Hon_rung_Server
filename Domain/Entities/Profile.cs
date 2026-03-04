using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Profile
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid AccountId { get; set; } // Foreign key to Account

    [ForeignKey("AccountId")]
    public Account Account { get; set; }

    [Required]
    [MaxLength(50)]
    public string Nickname { get; set; }
    
    public string AvatarUrl { get; set; } = string.Empty;

    public int Level { get; set; } = 1;
    public int Gold { get; set; } = 0;
    public int RankScore { get; set; } = 0;

}
