using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchedulingApplication.Data.Entities
{
    [Table("Coach")]
    public class Coach
    {
        [Key]
        public int Id { get; set; }
        public string? Image { get; set; }
        public string? Name { get; set; }
        public string? EmailAddress { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
