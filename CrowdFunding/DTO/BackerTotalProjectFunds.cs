using CrowdFunding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.DTO
{
    public class BackerIdTotalProjectFunds
    {
        public int BackerId { get; set; }
        public decimal TotalFunds { get; set;}
    }

    public class BackerTotalProjectFunds
    {
        public User Backer { get; set; }
        public decimal TotalFunds { get; set; }
    }
}
