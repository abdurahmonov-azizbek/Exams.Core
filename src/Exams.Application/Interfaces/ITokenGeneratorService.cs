using Exams.Domain.Entities;

namespace Exams.Application.Interfaces
{
    public interface ITokenGeneratorService
    {
        string GetToken(User user);
    }
}
