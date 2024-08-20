using Exams.Domain.Models;

namespace Exams.Application.Interfaces
{
    public interface IAccountService
    {
        ValueTask<bool> RegisterAsync(RegisterDetails registerDetails);
        ValueTask<string> LoginAsync(LoginDetails loginDetails);
        ValueTask<bool> SendVerificationCodeAsync(Guid userId);
        ValueTask<bool> VerifyCodeAsync(Guid userId, string code);
        ValueTask<bool> UpdatePasswordAsync(Guid userId, UpdatePasswordDetails updatePasswordDetails);
        ValueTask<bool> SendResetCodeAsync(Guid userId);
    }
}
