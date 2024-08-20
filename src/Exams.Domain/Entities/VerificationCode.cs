using Exams.Domain.Entities.Common;

namespace Exams.Domain.Entities
{
    public class VerificationCode : EntityBase
    {
        public string Code { get; set; } = default!;
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
