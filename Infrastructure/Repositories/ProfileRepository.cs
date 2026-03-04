using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProfileRepository : IProfileRepository
{
    private readonly AppDbContext _context;

    public ProfileRepository(AppDbContext context)
    {
        _context = context;
    }

    //Tạo hồ sơ người dùng
    public async Task<Profile?> Create(string id)
    {
        if (!Guid.TryParse(id, out var profileId))
        {
            throw new ArgumentException($"ID '{id}' không phải là GUID hợp lệ.");
        }

        var existingProfile = await _context.Profiles.FindAsync(profileId);
        if (existingProfile != null)
        {
            return existingProfile;
        }

        var newProfile = new Profile
        {
            Id = profileId,
            AccountId = profileId,
            Nickname = $"User_{id.Substring(0, 8)}",
        };

        _context.Profiles.Add(newProfile);
        await _context.SaveChangesAsync();

        return newProfile;
    }

    // Xóa hồ sơ người dùng
    public async Task<bool> Delete(string id)
    {
        if (!Guid.TryParse(id, out var guidId))
        {
            return false;
        }

        var profile = await _context.Profiles.FindAsync(guidId);
        if (profile == null)
        {
            return false;
        }

        _context.Profiles.Remove(profile);
        await _context.SaveChangesAsync();
        return true;
    }

    // Tìm hồ sơ người dùng theo ID
    public async Task<Profile?> FindById(string id)
    {
        if (!Guid.TryParse(id, out var guidId))
        {
            return null; // ID không hợp lệ thì coi như không tìm thấy
        }

        return await _context.Profiles.FindAsync(guidId);
    }

    // Cập nhật hồ sơ người dùng
    public async Task<Profile> Update(Profile profile)
    {
        _context.Profiles.Update(profile);
        await _context.SaveChangesAsync();
        return profile;
    }
}