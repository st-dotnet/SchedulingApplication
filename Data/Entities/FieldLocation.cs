using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchedulingApplication.Data.Entities
{
    [Table("FieldLocation")]
    public class FieldLocation
    {
        [Key]
        public int Id { get; set; }
        public string? Name  { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
