using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Account
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    public DateTime CreateAt { get; set; } = DateTime.UtcNow;


}
