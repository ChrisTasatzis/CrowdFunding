using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public decimal Goal { get; set; }
        public decimal Progress { get; set; }   
        public bool isActive { get; set; }
        public User ProjectCreator { get; set; }
        public List<User> Backers { get; set; }
        public List<Post> Posts { get; set; }
        public List<FundingPackage> FundingPackages { get; set;}
        public List<Media> Medias { get; set;}
    }
}
