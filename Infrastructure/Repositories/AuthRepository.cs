using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }

    //Tạo tài khoản
    public async Task<Account> Create(string email, string passwordHash)
    {
        var newAccount = new Account
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordHash = passwordHash
        };

        _context.Accounts.Add(newAccount);
        await _context.SaveChangesAsync();

        return newAccount;
    }

    //Tìm theo tên
    public async Task<Account?> FindByEmail(string email)
    {
        return await _context.Accounts
            .FirstOrDefaultAsync(a => a.Email == email);
    }

    public async Task<bool> Delete(Guid id)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account == null) 
            return false;

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
        return true;
    }

    //Tìm theo Id
    public async Task<Account?> FindById(Guid id)
    {
        return await _context.Accounts.FindAsync(id);
    }

    //Cập nhật tài khoản
    public async Task<Account> Update(Account account)
    {
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
        return account;
    }

    //Thay đổi mật khẩu
    public async Task<bool> UpdatePassword(Guid id, string newPasswordHash)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account == null) return false;

        account.PasswordHash = newPasswordHash;

        await _context.SaveChangesAsync();
        return true;
    }
}