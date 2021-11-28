using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Models
{
    public class ProjectBacker
    {
        public int ProjectId { get; set; }
        public int BackerId { get; set; }
        public decimal Contribution { get; set; }
        public DateTime DateTime { get; set; }
    }
}
