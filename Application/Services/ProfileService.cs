using Application.Interfaces;
using Domain.Interfaces;
using Shared.DTOs.Response;

namespace Application.Services;

public class ProfileService : IProfileService
{
    private readonly IProfileRepository _profileRepo;
    private readonly StorageService _storageService;

    public ProfileService(IProfileRepository profileRepo, StorageService storageService)
    {
        _profileRepo = profileRepo;
        _storageService = storageService;
    }

    public async Task<ProfileResponse?> GetProfileById(Guid id)
    {
        var profile = await _profileRepo.FindById(id.ToString());

        if (profile == null) 
            return null;

        string avatarUrl = string.IsNullOrEmpty(profile.AvatarUrl)
            ? "" // Hoặc đường dẫn ảnh mặc định
            : await _storageService.GetFileUrlAsync(profile.AvatarUrl);

        return new ProfileResponse
        {
            Id = profile.Id,
            Nickname = profile.Nickname ?? "Unknown",
            AvatarUrl = profile.AvatarUrl,
            Level = profile.Level,
            Gold = profile.Gold,
            RankScore = profile.RankScore
        };
    }

    public async Task<string> UploadAvatarAsync(Guid userId, Stream fileStream, string fileName, string contentType)
    {
        // 1. Upload file lên MinIO (StorageService đã xử lý việc tạo bucket)
        // Đặt tên file unique để tránh trùng lặp: {UserId}_{OriginalFileName}
        var objectName = $"{userId}_{fileName}";
        var storedFileName = await _storageService.UploadFileAsync(fileStream, objectName, contentType);

        // 2. Cập nhật tên file vào Database
        var profile = await _profileRepo.FindById(userId.ToString());
        if (profile != null)
        {
            profile.AvatarUrl = storedFileName; // Chỉ lưu tên file
            await _profileRepo.Update(profile);
        }

        // 3. Trả về URL để hiển thị ngay lập tức
        return await _storageService.GetFileUrlAsync(storedFileName);
    }
}
