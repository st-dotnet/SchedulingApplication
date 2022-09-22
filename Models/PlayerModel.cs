namespace SchedulingApplication.Models
{
    public class PlayerModel
    {
        public int Id { get; set; }
        public string? PlayerName { get; set; }
        public string? EmailAddress { get; set; }
        public int? Age { get; set; } 
        public int? TeamId { get; set; }
        public string? Team { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public string? Image { get; set; }
        public IFormFile? BaseImage { get; set; }
        public bool? IsClubPassPlayer { get; set; }
    }
}
