using Domain.Entities;

namespace Domain.Interfaces;

public interface IProfileRepository
{
    Task<Profile?> FindById(string Id);
    Task<Profile?> Create(string Id);
    Task<Profile> Update(Profile profile);
    Task<bool> Delete(string Id);
}
