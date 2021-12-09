using CrowdFunding.Models;
using System.ComponentModel.DataAnnotations;

namespace CrowdFundingMVC.Models.ProjectController
{
    public class SearchViewModel
    {
        public List<Project>? Projects { get; set; }
        [Required]
        public string? SearchTerm { get; set; }
        public int Pages { get; set; }
    }
}
