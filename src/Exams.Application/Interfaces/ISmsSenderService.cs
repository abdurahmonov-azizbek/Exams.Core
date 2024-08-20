using Exams.Domain.Models;

namespace Exams.Application.Interfaces
{
    public interface ISmsSenderService
    {
        ValueTask<bool> SendAsync(SendSmsRequest smsRequest);
    }
}
