using Exams.Domain.Dtos;
using Exams.Domain.Entities;

namespace Exams.Application.Interfaces
{
	public interface IResetCodeService
	{
		ValueTask<bool> CreateAsync(ResetCodeDto resetCodeDto);
        IQueryable<ResetCode> GetAll();
        ValueTask<ResetCode?> GetByIdAsync(Guid resetCodeId);
	}
}