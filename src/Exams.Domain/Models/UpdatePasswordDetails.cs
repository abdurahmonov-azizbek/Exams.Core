namespace Exams.Domain.Models
{
	public class UpdatePasswordDetails
	{
		public string OldPassword { get; set; } = default!;
		public string NewPassword { get; set; } = default!;
		public string ConfirmNewPassword { get; set; } = default!;
	}
}