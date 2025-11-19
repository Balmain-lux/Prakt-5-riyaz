using System.ComponentModel.DataAnnotations;

namespace Prakt_5_riyaz.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Связи
        public int TaskId { get; set; }
        public ProjectTask Task { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
