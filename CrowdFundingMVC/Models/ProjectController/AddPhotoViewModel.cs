using System.ComponentModel.DataAnnotations;

namespace CrowdFundingMVC.Models.ProjectController
{
    public class AddPhotoViewModel
    {
        [Required]
        public IFormFile? Photo { get; set; }

        [FileExtensions(Extensions = "jpg,jpeg", ErrorMessage = "The provided file must have JPEG format.")]
        public string FileName => Photo?.FileName;
        [Required]
        public int ProjectId { get; set; }
    }
}
