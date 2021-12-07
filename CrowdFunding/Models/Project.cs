using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Models
{
    public class Project
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public decimal Goal { get; set; }
        public string? Thumbnail { get; set; }
        public decimal? Progress { get; set; } = 0;
        public bool? isActive { get; set; } = true;
        public DateTime DateTime { get; set; }
        public User? ProjectCreator { get; set; }
        public List<User>? Backers { get; set; }
        public List<Post>? Posts { get; set; }
        public List<FundingPackage>? FundingPackages { get; set; }
        public List<Photo>? Photos { get; set; }
        public List<Video>? Videos { get; set; }

    }
}