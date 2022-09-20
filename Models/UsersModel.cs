using System.ComponentModel.DataAnnotations;

namespace SchedulingApplication.Models
{
    public class UsersModel
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public int? RoleId { get; set; }

        public string? Password { get; set; }
    }
}
