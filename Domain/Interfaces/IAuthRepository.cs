using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IAuthRepository
{
    Task<Account?> FindById(Guid id);
    Task<Account?> FindByEmail(string email);
    Task<Account> Create(string username, string passwordHash);
    Task<Account> Update(Account account);
    Task<bool> Delete(Guid id);
    Task<bool> UpdatePassword(Guid id, string newPasswordHash);
}
