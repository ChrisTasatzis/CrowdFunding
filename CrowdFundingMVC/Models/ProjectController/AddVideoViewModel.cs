using System.ComponentModel.DataAnnotations;

namespace CrowdFundingMVC.Models.ProjectController
{
    public class AddVideoViewModel
    {
        [Required]
        [RegularExpression(@"^(https:\/\/)?(www.)?(youtube.com|youtu.be)\/(watch)(\?v=)(.{11})$",
         ErrorMessage = "Provide a valid Youtube video URL.")]
        public string? URL { get; set; }
        public int ProjectId { get; set; }
    }
}
