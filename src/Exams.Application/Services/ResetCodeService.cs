using Exams.Application.Interfaces;
using Exams.Data.DbContexts;
using Exams.Domain.Dtos;
using Exams.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Exams.Application.Services
{
    public class ResetCodeService(AppDbContext context) : IResetCodeService
    {
        public async ValueTask<bool> CreateAsync(ResetCodeDto resetCodeDto)
        {
            try
            {
                var resetCode = new ResetCode
                {
                    Code = resetCodeDto.Code,
                    UserId = resetCodeDto.UserId,
                };

                await context.ResetCodes.AddAsync(resetCode);
                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<ResetCode> GetAll()
        {
            return context.ResetCodes.AsQueryable();
        }

        public async ValueTask<ResetCode?> GetByIdAsync(Guid resetCodeId)
        {
            var resetCode = await context.ResetCodes.FirstOrDefaultAsync(code => code.UserId == resetCodeId);

            return resetCode;
        }
    }
}
