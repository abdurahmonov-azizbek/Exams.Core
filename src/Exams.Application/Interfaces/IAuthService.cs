using Exams.Domain.Models;

namespace Exams.Application.Interfaces
{
    public interface IAuthService
    {
        ValueTask<bool> RegisterAsync(RegisterDetails registerDetails);
        ValueTask<Response> LoginAsync(LoginDetails loginDetails);
    }
}
