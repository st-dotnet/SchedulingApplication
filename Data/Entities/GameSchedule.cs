using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchedulingApplication.Data.Entities
{ 
    public class GameSchedule
    { 
        public int Id { get; set; }
        public string? Name { get; set; }
        public int TeamId { get; set; }
        public int PlayingAgainstId { get; set; }
        public int FieldLocationId { get; set; }
        public int GameTypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CreatedBy { get; set; }
        public bool? IsOverlap { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime CreatedOn { get; set; }
        public virtual FieldLocation? FieldLocation { get; set; }
        public virtual Team? Team { get; set; }
        public virtual Team? PlayingAgainstTeam { get; set; }
        public virtual GameType? GameType { get; set; }
    }
}
