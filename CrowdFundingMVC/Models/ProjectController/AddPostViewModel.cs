using System.ComponentModel.DataAnnotations;

namespace CrowdFundingMVC.Models.ProjectController
{
    public class AddPostViewModel
    {
        [Required]
        public string? Text { get; set; }
        public int ProjectId { get; set; }
    }
}
