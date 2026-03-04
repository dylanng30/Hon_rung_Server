using Shared.DTOs.Response;

namespace Application.Interfaces;

public interface IProfileService
{
    Task<ProfileResponse?> GetProfileById(Guid id);
    Task<string> UploadAvatarAsync(Guid id, Stream fileStream, string fileName, string contentType);
}
