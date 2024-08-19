using Exams.Domain.Entities.Common;
using Exams.Domain.Enums;
using System.Globalization;

namespace Exams.Domain.Entities
{
    public class User : EntityBase
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public Role Role { get; set; } = Role.User;
    }
}
