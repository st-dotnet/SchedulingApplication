using System.ComponentModel.DataAnnotations.Schema;

namespace SchedulingApplication.Data.Entities
{
    [Table("GameType")]
    public class GameType
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
