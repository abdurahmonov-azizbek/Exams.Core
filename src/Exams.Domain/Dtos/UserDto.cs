using Exams.Domain.Enums;

namespace Exams.Domain.Dtos
{
    public class UserDto
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Password { get; set; } = default!;
        public Role Role { get; set; } = Role.User;
        public bool IsVerified { get; set; }
    }
}
