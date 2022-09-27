namespace SchedulingApplication.Models
{
    public class CoachModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? EmailAddress { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public string? Image { get; set; }
        public IFormFile? BaseImage { get; set; }
    }
}
