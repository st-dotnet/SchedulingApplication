using System.ComponentModel.DataAnnotations;

namespace SchedulingApplication.Models
{
    public class ResetPasswordModel
	{
		public string? Password { get; set; }

		public string? ConfirmPassword { get; set; }

		public string? Email { get; set; }
	}
}
