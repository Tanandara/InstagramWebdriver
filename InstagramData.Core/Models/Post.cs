using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramData.Core.Models
{
    public class Post
    {
        public string MediaURL { get; set; }
        public List<string> Images { get; set; }
        public string Caption { get; set; }
        public List<Tag> Tags { get; set; }
        public List<string> PictureTags { get; set; }
        public List<ReferenceProfile> Refs { get; set; }
        public List<Comment> Comments { get; set; }
        public int CommentCount { get; set; }
        public DateTime PostDate { get; set; }

        public Post()
        {
            this.Images = new List<string>();
            this.Tags = new List<Tag>();
            this.Comments = new List<Comment>();
            this.Refs = new List<ReferenceProfile>();
            this.PictureTags = new List<string>();
        }

    }
}
