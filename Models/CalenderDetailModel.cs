namespace SchedulingApplication.Models
{
    public class CalenderDetailModel 
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; } 
        public string? HomeTeam { get; set; }
        public string? AwayTeam { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }  
        public string? Location { get; set; } 
        public string? ClassName { get; set; }
        public  bool? IsOverlap { get; set; }
        public string StartStr 
        { 
            get
            {
                return Start.GetValueOrDefault().ToString("g");
            }
        }
        public string EndStr
        {
            get
            {
                return End.GetValueOrDefault().ToString("g");
            }
        }
    }
}
