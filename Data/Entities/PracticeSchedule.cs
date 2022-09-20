using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchedulingApplication.Data.Entities
{
    [Table("PracticeSchedule")]
    public class PracticeSchedule
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int TeamId { get; set; }
        public int PlayingAgainstId { get; set; }
        public int FieldLocationId { get; set; }
        public DateTime Date { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual FieldLocation? FieldLocation { get; set; }
        public virtual Team? Team { get; set; }

    }
}
