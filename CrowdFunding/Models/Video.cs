using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public DateTime DateTime { get; set; }
        public Project Project { get; set; }
    }
}
