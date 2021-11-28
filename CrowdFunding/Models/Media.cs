using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Models
{
    public class Media
    {
        public int Id { get; set; }
        public string URI { get; set; }
        public bool IsVideo { get; set; }
        public DateTime DateTime { get; set; }
        public Project Project { get; set; }
    }
}
