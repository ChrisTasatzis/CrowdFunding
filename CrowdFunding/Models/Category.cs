using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Models
{
    public enum Category
    {
        [Description("Art")] Art,
        [Description("Comics")] Comics,
        [Description("Illustration")] Illustration,
        [Description("Design")] Design,
        [Description("Tech")] Tech,
        [Description("Film")] Film,
        [Description("Food")] Food,
        [Description("Craft")] Craft,
        [Description("Games")] Games,
        [Description("Music")] Music,
        [Description("Publishing")] Publishing
    }
}
