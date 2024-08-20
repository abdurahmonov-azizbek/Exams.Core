namespace Exams.Domain.Dtos
{
    public class VerificationCodeDto
    {
        public string Code { get; set; } = default!;
        public Guid UserId { get; set; }
    }
}
