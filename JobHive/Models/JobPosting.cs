using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobHive.Models
{
    public class JobPosting
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Company { get; set; }

        public DateTime PostedDate { get; set; } = DateTime.UtcNow;

        public bool IsApproved { get; set; } = false;

        [Required]
        public string UserId { get; set; }

        // helps to create a relationship between the JobPosting and the IdentityUser
        // reduces the need to write custom queries to get the user details
        [ForeignKey(nameof(UserId)]
        public IdentityUser User { get; set; }

    }
}
