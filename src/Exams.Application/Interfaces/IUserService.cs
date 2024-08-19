using Exams.Domain.Dtos;
using Exams.Domain.Entities;

namespace Exams.Application.Interfaces
{
    public interface IUserService
    {
        ValueTask<bool> CreateAsync(UserDto userDto);
        IQueryable<User> GetAll();
        ValueTask<User?> GetByIdAsync(Guid userId);
        ValueTask<bool> UpdateAsync(UserDto userDto, Guid userId);
        ValueTask<bool> DeleteByIdAsync(Guid userId);
    }
}
