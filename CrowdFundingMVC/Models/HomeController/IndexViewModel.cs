using CrowdFunding.Models;

namespace CrowdFundingMVC.Models.HomeController
{
    public class IndexViewModel
    {
        public List<Project>? Projects { get; set; }
        public Project? ReturnProject { get; set; }
    }
}
