using InstagramData.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramData.MergeJSON.Models
{
    public class InstagramMergeProfile
    {
        public string URL { get; set; }
        public string Name { get; set; }
        public List<InstagramProfile> Followings { get; set; }

        public InstagramMergeProfile()
        {
            this.Followings = new List<InstagramProfile>();
        }
    }
}
