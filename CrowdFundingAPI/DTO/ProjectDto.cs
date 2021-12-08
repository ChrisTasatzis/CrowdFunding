using CrowdFunding.Models;

namespace CrowdFundingAPI.DTO
{
    public class ProjectDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Goal { get; set; }
        public string? Thumbnail { get; set; }
        public decimal? Progress { get; set; } = 0;
        public bool? isActive { get; set; } = true;
        public User? ProjectCreator { get; set; }
        public List<User>? Backers { get; set; }
        public List<Post>? Posts { get; set; }
        public List<FundingPackage>? FundingPackages { get; set; }
        public List<Photo>? Photos { get; set; }
    }
}
