using Exams.Application.Interfaces;
using Exams.Domain.Models;

namespace Exams.Application.Services
{
    public class SmsSenderService : ISmsSenderService
    {
        public ValueTask<bool> SendAsync(SendSmsRequest smsRequest)
        {
            //send sms logic here

            return new(true);
        }
    }
}
