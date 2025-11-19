using System.ComponentModel.DataAnnotations;

namespace Prakt_5_riyaz.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Priority { get; set; } = "Medium";

        public string Status { get; set; } = "ToDo";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime Deadline { get; set; }

        // Связи
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
