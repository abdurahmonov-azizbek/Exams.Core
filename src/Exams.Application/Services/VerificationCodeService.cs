using Exams.Application.Interfaces;
using Exams.Data.DbContexts;
using Exams.Domain.Dtos;
using Exams.Domain.Entities;
using Exams.Domain.Helper;
using Microsoft.EntityFrameworkCore;

namespace Exams.Application.Services
{
    public class VerificationCodeService(AppDbContext context) : IVerificationCodeService
    {
        public async ValueTask<bool> CreateAsync(VerificationCodeDto verificationCodeDto)
        {
            try
            {
                var verificationCode = new VerificationCode
                {
                    Id = Guid.NewGuid(),
                    Code = verificationCodeDto.Code,
                    UserId = verificationCodeDto.UserId,
                    CreatedDate = Helper.GetCurrentDateTime()
                };

                await context.VerificationCodes.AddAsync(verificationCode);
                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<VerificationCode> GetAll()
        {
            return context.VerificationCodes.AsQueryable();
        }

        public async ValueTask<VerificationCode?> GetByIdAsync(Guid verificationCodeId)
        {
            var code = await context.VerificationCodes.FirstOrDefaultAsync(code => code.Id == verificationCodeId);
            return code;
        }
    }
}
