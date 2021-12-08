
using CrowdFunding.Models;
using System.ComponentModel.DataAnnotations;

namespace CrowdFundingMVC.Models.ProjectView
{
    public class CreateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public decimal Goal { get; set; }
        [Required]
        public IFormFile Thumbnail { set; get; }
    }
}
