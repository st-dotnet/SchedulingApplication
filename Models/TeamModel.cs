namespace SchedulingApplication.Models
{
    public class TeamModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsLookingClubPassPlayer { get; set; }
        public string? AgeGroup { get; set; }
        public int? CoachId { get; set; }
        public string Coach { get; set; }
        public int CreatedBy { get; set; }
        public string? Image { get; set; }
        public IFormFile? BaseImage { get; set; }
    }
}
