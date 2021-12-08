using CrowdFunding.Models;

namespace CrowdFundingMVC.Models.ProjectController
{
    public class SearchViewModel
    {
        public List<Project> Projects { get; set; }
        public string SearchTerm { get; set; }
        public int Pages { get; set; }
    }
}
