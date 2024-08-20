using Exams.Domain.Dtos;
using Exams.Domain.Entities;

namespace Exams.Application.Interfaces
{
    public interface IVerificationCodeService
    {
        ValueTask<bool> CreateAsync(VerificationCodeDto verificationCodeDto);
        IQueryable<VerificationCode> GetAll();
        ValueTask<VerificationCode?> GetByIdAsync(Guid verificationCodeId);
    }
}
