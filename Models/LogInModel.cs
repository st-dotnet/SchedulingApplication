using System.ComponentModel.DataAnnotations;

namespace SchedulingApplication.Models
{
    public class LogInModel
    {

        [Required]
        [EmailAddress]
        [Display(Name = "EmailAddress")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
