namespace Exams.Domain.Dtos
{
	public class ResetCodeDto
	{
		public string Code { get; set; } = default!;
		public Guid UserId { get; set; }
	}
}