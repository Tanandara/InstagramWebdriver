using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramData.Core.Models
{
    public class InstagramProfile
    {
        public string URL { get; set; }
        public string Name { get; set; }
        public int FollowerCount { get; set; }
        public int FollowingCount { get; set; }
        public List<Following> Followings { get; set; }
        public List<Follower> Followers { get; set; }
        public List<Post> Posts { get; set; }

        public InstagramProfile(string url,string name)
        {
            this.URL = url;
            this.Name = name;
            this.Followings = new List<Following>();
            this.Followers = new List<Follower>();
            this.Posts = new List<Post>();
        }
    }
}
