namespace Exams.Domain.Models
{
    public class SendSmsRequest
    {
        public string Message { get; set; } = default!;
        public string ToPhoneNumber { get; set; } = default!;
    }
}
