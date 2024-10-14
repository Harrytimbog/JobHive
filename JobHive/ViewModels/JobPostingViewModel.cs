using System.ComponentModel.DataAnnotations;

namespace JobHive.ViewModels
{
    public class JobPostingViewModel
    {
        [Required]
        public required string Title { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public required string Location { get; set; }
        [Required]
        public required string Company { get; set; }

    }
}
