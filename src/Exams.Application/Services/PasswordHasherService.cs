using Exams.Application.Interfaces;
using BC = BCrypt.Net.BCrypt;

namespace Exams.Application.Services
{
    public class PasswordHasherService : IPasswordHasherService
    {
        public string Hash(string password)
        {
            return BC.HashPassword(password);
        }

        public bool Verify(string password, string hash)
        {
            return BC.Verify(password, hash);
        }
    }
}
