using System.ComponentModel.DataAnnotations;

namespace CrowdFundingMVC.Models.ProjectController
{
    public class AddPhotoViewModel
    {
        [Required]
        public IFormFile? Photo { get; set; }
        [Required]
        public int ProjectId { get; set; }
    }
}
