using System.ComponentModel.DataAnnotations;

namespace CrowdFundingMVC.Models.ProjectController
{
    public class AddFPViewModel
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Reward { get; set; }
        [Required]
        [Range(double.Epsilon, (double)decimal.MaxValue, ErrorMessage = "Price should be bigger than 0€")]
        public decimal Price { get; set; }
        public int ProjectId { get; set; }
    }
}
