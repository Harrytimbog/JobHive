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
        public required string Title { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public required string Location { get; set; }
        [Required]
        public required string Company { get; set; }

        public DateTime PostedDate { get; set; } = DateTime.UtcNow;

        public bool IsApproved { get; set; } = false;

        [Required]
        public required string UserId { get; set; }

        // helps to create a relationship between the JobPosting and the IdentityUser
        // reduces the need to write custom queries to get the user details
        [ForeignKey(nameof(UserId))]
        public required IdentityUser User { get; set; }

    }
}
