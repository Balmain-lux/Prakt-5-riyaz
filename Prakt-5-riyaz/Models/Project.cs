using System.ComponentModel.DataAnnotations;

namespace Prakt_5_riyaz.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        public string Status { get; set; } = "Planning";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}
