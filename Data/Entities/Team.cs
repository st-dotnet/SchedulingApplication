using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchedulingApplication.Data.Entities
{
    public class Team
    { 
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsLookingClubPassPlayer { get; set; }
        public string? AgeGroup { get; set; }
        public int CoachId { get; set; }
        public int CreatedBy { get; set; }
        public byte[]? Image { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual Coach? Coach { get; set; } 
        public virtual ICollection<GameSchedule>? HomeSchedules { get; set; }
        public virtual ICollection<GameSchedule>? PlayingAgainstSchedules { get; set; }
    }
}
