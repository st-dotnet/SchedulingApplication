using System.ComponentModel.DataAnnotations;

namespace SchedulingApplication.Models
{
	public class ForgotPasswordModel
	{
		[Required]
		[EmailAddress]
		public string? Email { get; set; }
	}
}
