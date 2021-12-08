using CrowdFunding.Models;

namespace CrowdFundingMVC.Models.ProjectController
{
    public class CategoryViewModel
    {
        public List<Project> Projects { get; set; }
        public int Category { get; set; }
        public int Pages { get; set; }
    }
}
