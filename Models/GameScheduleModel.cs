namespace SchedulingApplication.Models
{
    public class GameScheduleModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int TeamId { get; set; }
        public int PlayingAgainstId { get; set; }
        public int FieldLocationId { get; set; }
        public int GameTypeId { get; set; }
        public bool IsOverlap { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
