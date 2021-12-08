using CrowdFunding.Models;

namespace CrowdFundingAPI.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<Project> BackedProjects { get; set; }
        public virtual List<Project> CreatedProjects { get; set; }
    }
}
