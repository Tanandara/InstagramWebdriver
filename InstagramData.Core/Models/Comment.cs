using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramData.Core.Models
{
    public class Comment
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public List<Tag> Tags { get; set; }
        public List<ReferenceProfile> Refs { get; set; }

        public Comment()
        {
            this.Tags = new List<Tag>();
            this.Refs = new List<ReferenceProfile>();
        }

    }
}
