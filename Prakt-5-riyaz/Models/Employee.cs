using System.ComponentModel.DataAnnotations;

namespace Prakt_5_riyaz.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string ContactInfo { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
    }

