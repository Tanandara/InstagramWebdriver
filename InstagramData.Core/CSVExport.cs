using InstagramData.Core.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;

public class CSVExport
{
    private readonly InstagramProfile _instagramProfile;

    public CSVExport(InstagramProfile instagramProfile)
    {
        this._instagramProfile = instagramProfile;
    }

    public void CreateGenaral()
    {
        var content = "";

        content = string.Format("{0},{1},{2},{3}", _instagramProfile.Name, _instagramProfile.URL, _instagramProfile.FollowerCount, _instagramProfile.FollowingCount);

        content += Environment.NewLine;

        File.AppendAllText("General.csv", content, Encoding.UTF8);
    }

    public void CreateFollowing()
    {
        foreach (var following in _instagramProfile.Followings)
        {
            var content = "";

            content = string.Format("{0},{1},{2}", _instagramProfile.Name, following.Name, following.URL);

            content += Environment.NewLine;

            File.AppendAllText("Following.csv", content, Encoding.UTF8);
        }
    }
    public void CreateMedia()
    {
        foreach (var media in _instagramProfile.Posts)
        {
            var content = "";

            var mediaTags = media.Tags.Select(t => t.TagId).ToList();
            var mediaTagsAfterJoinPipe = string.Join("|", mediaTags);

            var mediaRefs = media.Tags.Select(t => t.TagId).ToList();
            var mediaRefsAfterJoinPipe = string.Join("|", mediaRefs);

            foreach (var comment in media.Comments)
            {
                var commentTags = comment.Tags.Select(t => t.TagId).ToList();
                var commentTagsAfterJoinPipe = string.Join("|", commentTags);

                var commentRefs = comment.Tags.Select(t => t.TagId).ToList();
                var commentRefsAfterJoinPipe = string.Join("|", commentRefs);

                content = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                    _instagramProfile.Name,
                    media.MediaURL,
                    media.Caption?.Replace(System.Environment.NewLine, "replacement text").Replace("\"", "").Replace(",", ""),
                    mediaTagsAfterJoinPipe,
                    mediaRefsAfterJoinPipe,
                    comment.Text?.Replace("\"", "").Replace(",", ""),
                    commentTagsAfterJoinPipe,
                    commentRefsAfterJoinPipe);

                content += Environment.NewLine;

                File.AppendAllText("Comment.csv", content, Encoding.UTF8);
            }

        }
    }

    
}