using Exams.Application.Interfaces;
using Exams.Data.DbContexts;
using Exams.Domain.Dtos;
using Exams.Domain.Entities;
using Exams.Domain.Helper;
using Microsoft.EntityFrameworkCore;

namespace Exams.Application.Services
{
    public class UserService(AppDbContext context) : IUserService
    {
        public async ValueTask<bool> CreateAsync(UserDto userDto)
        {
            try
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    PhoneNumber = userDto.PhoneNumber,
                    Password = userDto.Password,
                    Role = userDto.Role,
                    CreatedDate = Helper.GetCurrentDateTime()
                };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async ValueTask<bool> DeleteByIdAsync(Guid userId)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(user => user.Id == userId);

                if (user is null)
                {
                    return false;
                }

                context.Users.Remove(user);
                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<User> GetAll()
        {
            var users = context.Users.AsQueryable();

            return users;
        }

        public async ValueTask<User?> GetByIdAsync(Guid userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.Id == userId);

            return user;
        }

        public async ValueTask<bool> UpdateAsync(UserDto userDto, Guid userId)
        {
            try
            {
                var foundUser = await context.Users.FirstOrDefaultAsync(user => user.Id == userId);

                if (foundUser is null)
                {
                    return false;
                }

                foundUser.FirstName = userDto.FirstName;
                foundUser.LastName = userDto.LastName;
                foundUser.PhoneNumber = userDto.PhoneNumber;
                foundUser.Role = userDto.Role;
                foundUser.Password = userDto.Password;

                context.Users.Update(foundUser);
                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
