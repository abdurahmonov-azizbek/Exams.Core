namespace Exams.Domain.Entities
{
	public class ResetCode
	{
		public string Code { get; set; } = default!;
		public Guid UserId { get; set; }
		public virtual User? User { get; set; }
	}
}