using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(string userId, string email);
    string GenerateRefreshToken();
}
