using CrowdFunding.DTO;
using CrowdFunding.Models;

namespace CrowdFundingMVC.Models.ProjectController
{
    public class DetailsViewModel
    {
        public Project Project { get; set; }
        public List<BackerTotalProjectFunds> BackerFunds { get; set; }
        public int ProjectId { get; set; }
        public int FundingPackageId { get; set; }
    }
}
