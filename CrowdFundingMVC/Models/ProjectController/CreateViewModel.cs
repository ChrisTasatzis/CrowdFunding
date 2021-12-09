
using CrowdFunding.Models;
using System.ComponentModel.DataAnnotations;

namespace CrowdFundingMVC.Models.ProjectView
{
    public class CreateViewModel
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Category { get; set; }
        [Required]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Goal should be bigger than 0€")]
        public decimal Goal { get; set; }
        [Required]
        public IFormFile? Thumbnail { set; get; }

        [FileExtensions(Extensions = "jpg,jpeg", ErrorMessage = "The provided file must have JPEG format.")]
        public string FileName => Thumbnail?.FileName;
    }
}
