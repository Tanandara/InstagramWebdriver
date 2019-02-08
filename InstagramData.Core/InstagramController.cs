using InstagramData.Core.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InstagramData.Core
{
    public class InstagramController
    {
        private readonly List<InstagramProfile> _instagramProfile;
        private readonly User _userLogin;
        private readonly int _scrollTimes;

        public FirefoxDriver Driver { get; private set; }

        public InstagramController(User userLogin, List<InstagramProfile> instagramProfiles, int scrollTimes = 5)
        {
            this._instagramProfile = instagramProfiles;
            this._userLogin = userLogin;
            this.Driver = new FirefoxDriver();
            this._scrollTimes = scrollTimes;
        }

        public void Run()
        {
            Login();
            foreach (var ig in _instagramProfile)
            {
                Driver.Navigate().GoToUrl(ig.URL);
                Thread.Sleep(1000);
                GetFollowerAndFollowing(ig);
                GetAllPostLink(ig);
                GetAllPostDetail(ig);
            }

        }



        private void Login()
        {
            Driver.Url = "https://www.instagram.com/accounts/login/";
            Driver.Navigate();
            Thread.Sleep(2000);

            var usernameTextbox = Driver.FindElement(By.Name("username"));
            var passwordTextbox = Driver.FindElement(By.Name("password"));

            usernameTextbox.SendKeys(_userLogin.Username);
            passwordTextbox.SendKeys(_userLogin.Password);
            Thread.Sleep(2000);

            var Buttons = Driver.FindElements(By.CssSelector("button"));
            var button = Buttons.Where(b => b.Text == "Log in").FirstOrDefault();

            button.Click();

            Thread.Sleep(2000);
        }



        private void GetFollowerAndFollowing(InstagramProfile igProfile)
        {
            try
            {
                Thread.Sleep(2000);
                var selectedAllA = Driver.FindElements(By.CssSelector("a"));
                var followersTag = selectedAllA.Where(a => a.GetAttribute("href").Contains("/followers")).FirstOrDefault();
                var followerCount = followersTag.FindElement(By.TagName("span")).GetAttribute("title");
                Console.WriteLine("Follower: {0}", followerCount);

                igProfile.FollowerCount = int.Parse(followerCount.Replace(",", ""));


                #region Follower
                followersTag.Click();
                Thread.Sleep(2000);

                //.isgrP
                ((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, 100)");
                Thread.Sleep(500);
                ((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, 200)");
                Thread.Sleep(500);
                ((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, 300)");
                Thread.Sleep(500);
                ((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, 400)");
                Thread.Sleep(500);
                ((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, 500)");
                Thread.Sleep(500);
                ((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, 600)");
                Thread.Sleep(500);
                ((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, 700)");
                Thread.Sleep(500);

                long flagFollower = 0;
                for (;;)
                {
                    ((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, document.querySelector('.isgrP').scrollHeight )");
                    Thread.Sleep(100);
                    var currentHeight = (long)((IJavaScriptExecutor)Driver).ExecuteScript("return document.querySelector('.isgrP').scrollHeight;");

                    if (flagFollower == currentHeight)
                    {
                        break;
                    }
                    else
                    {
                        flagFollower = currentHeight;
                    }

                    Thread.Sleep(1000);
                }


                var followersLinks = Driver.FindElements(By.CssSelector("li div a"));
                var followers = followersLinks.Where(f => f.GetAttribute("title") != "").ToList();

                var followersCount = followers.Count();
                igProfile.FollowingCount = followersCount;
                Console.WriteLine("Follower: {0}", followersCount);


                foreach (var follower in followers)
                {
                    var igFollower = new Follower
                    {
                        Name = follower.GetAttribute("title"),
                        URL = follower.GetAttribute("href")
                    };

                    igProfile.Followers.Add(igFollower);

                    Console.WriteLine("{0} - {1}", follower.GetAttribute("title"), follower.GetAttribute("href"));
                }

                Thread.Sleep(2000);

                var buttonCloseFollower = Driver.FindElement(By.CssSelector(".WaOAr > button"));
                buttonCloseFollower.Click();

                Thread.Sleep(2000);
                #endregion

                Thread.Sleep(2000);

                #region Following
                var followingTag = selectedAllA.Where(a => a.GetAttribute("href").Contains("/following")).FirstOrDefault();
                followingTag.Click();
                Thread.Sleep(2000);

                //.isgrP
                ((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, 100)");
                Thread.Sleep(500);
                ((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, 200)");
                Thread.Sleep(500);
                ((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, 300)");
                Thread.Sleep(500);
                ((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, 400)");
                Thread.Sleep(500);
                ((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, 500)");
                Thread.Sleep(500);
                ((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, 600)");
                Thread.Sleep(500);
                ((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, 700)");
                Thread.Sleep(500);

                long flagFollowing = 0;
                for (;;)
                {
                    ((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, document.querySelector('.isgrP').scrollHeight )");
                    Thread.Sleep(100);
                    var currentHeight = (long)((IJavaScriptExecutor)Driver).ExecuteScript("return document.querySelector('.isgrP').scrollHeight;");

                    if (flagFollowing == currentHeight)
                    {
                        break;
                    }
                    else
                    {
                        flagFollowing = currentHeight;
                    }

                    Thread.Sleep(500);
                }


                var followingsLinks = Driver.FindElements(By.CssSelector("li div a"));
                var followings = followingsLinks.Where(f => f.GetAttribute("title") != "").ToList();

                var followingsCount = followings.Count();
                igProfile.FollowingCount = followingsCount;
                Console.WriteLine("Following: {0}", followingsCount);


                foreach (var following in followings)
                {
                    var igFollowing = new Following
                    {
                        Name = following.GetAttribute("title"),
                        URL = following.GetAttribute("href")
                    };

                    igProfile.Followings.Add(igFollowing);

                    Console.WriteLine("{0} - {1}", following.GetAttribute("title"), following.GetAttribute("href"));
                }

                Thread.Sleep(2000);

                var buttonCloseFollowing = Driver.FindElement(By.CssSelector(".WaOAr > button"));
                buttonCloseFollowing.Click();

                Thread.Sleep(2000);
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetAllPostLink(InstagramProfile igProfile)
        {

            foreach (var i in Enumerable.Range(1, _scrollTimes))
            {
                Thread.Sleep(2000);
                ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");
            }



            var allLink = Driver.FindElements(By.CssSelector("a"));
            var mediasLink = allLink.Where(a => a.GetAttribute("href").Contains("/p/")).Select(p => p.GetAttribute("href")).ToList();

            igProfile.Posts = new List<Post>();

            foreach (var media in mediasLink)
            {
                var igPost = new Post
                {
                    MediaURL = media
                };

                igProfile.Posts.Add(igPost);

                Console.WriteLine(media);
            }

            Thread.Sleep(1000);
        }

        private void GetAllPostDetail(InstagramProfile ig)
        {
            foreach (var post in ig.Posts)
            {
                Driver.Navigate().GoToUrl(post.MediaURL);

                GetImages(post);

                GetPictureTags(post);

                GetPostDate(post);

                GetCaption(post);

                //LoadComments();

                //GetComments(post);
            }

        }

        private void GetPostDate(Post post)
        {
            try
            {
                try
                {
                    var timeElement = Driver.FindElement(By.CssSelector("._1o9PC.Nzb55"));
                    var dateString = timeElement.GetAttribute("title");
                    var dateTime = timeElement.GetAttribute("datetime");

                    var postDate = DateTime.Parse(dateTime);

                    post.PostDate = postDate;
                }
                catch { }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetPictureTags(Post post)
        {
            try
            {
                try
                {
                    var buttonTags = Driver.FindElement(By.CssSelector("._9AhH0"));
                    buttonTags.Click();
                    Thread.Sleep(1000);
                }
                catch { }

            FindTags:

                try
                {
                    var tagsElements = Driver.FindElements(By.CssSelector(".eg3Fv"));
                    if (tagsElements.Any())
                    {
                        foreach (var element in tagsElements)
                        {
                            if (string.IsNullOrEmpty(element.Text)) continue;

                            post.PictureTags.Add(element.Text);
                        }
                    }
                }
                catch { }



                try
                {
                    var buttonRight = Driver.FindElementByClassName("coreSpriteRightChevron");

                    buttonRight.Click();
                    Thread.Sleep(1000);

                    goto FindTags;
                }
                catch { }




            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetComments(Post post)
        {
            try
            {
                //var commentSection = Driver.FindElements(By.CssSelector("#react-root > section > main > div > div > article > div:nth-of-type(2) > div:nth-of-type(1) > ul > li > div > div > div > div > span"));
                var commentSection = Driver.FindElements(By.CssSelector("#react-root > section > main > div > div > article > div:nth-of-type(2) > div:nth-of-type(1) > ul > li > div > div > div span"));
                var comments = commentSection.Skip(1).ToList();
                var commentCount = comments.Count();

                post.CommentCount = commentCount;
                Console.WriteLine("Comment Count: {0}", commentCount);

                foreach (var comment in comments)
                {
                    var tags = comment.FindElements(By.TagName("a")).Where(m => m.GetAttribute("href").Contains("/explore/tags")).ToList();
                    var refs = comment.FindElements(By.TagName("a")).Where(m => !m.GetAttribute("href").Contains("/explore/tags")).ToList();
                    Console.WriteLine("comment: {0}", comment.Text);


                    var refsComment = new List<ReferenceProfile>();
                    var tagsComment = new List<Tag>();

                    foreach (var tag in tags)
                    {
                        tagsComment.Add(new Tag
                        {
                            TagId = tag.Text,
                            URL = tag.GetAttribute("href")
                        });
                        Console.WriteLine("tag: {0} {1}", tag.Text, tag.GetAttribute("href"));
                    }
                    foreach (var @ref in refs)
                    {
                        refsComment.Add(new ReferenceProfile
                        {
                            RefId = @ref.Text,
                            URL = @ref.GetAttribute("href")
                        });
                        Console.WriteLine("ref: {0} {1}", @ref.Text, @ref.GetAttribute("href"));
                    }

                    post.Comments.Add(new Comment
                    {
                        Refs = refsComment,
                        Tags = tagsComment,
                        Text = comment.Text
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadComments()
        {
            IWebElement buttonLoadMoreComment;
            try
            {
                buttonLoadMoreComment = Driver.FindElement(By.CssSelector("#react-root > section > main > div > div > article > div:nth-of-type(2) > div:nth-of-type(1) > ul > li:nth-child(2) > button"));
            }
            catch (Exception)
            {
                return;
            }

            for (;;)
            {
                try
                {
                    buttonLoadMoreComment.Click();
                    Thread.Sleep(1000);
                }
                catch (Exception)
                {
                    break;
                }
            }
        }

        private void GetCaption(Post post)
        {
            try
            {
                var mediaSection = Driver.FindElement(By.CssSelector("#react-root > section > main > div > div > article > div:nth-of-type(2) > div:nth-of-type(1) > ul > li:nth-child(1) > div span"));
                var mediaText = mediaSection.Text;
                var mediaTagAndReferrence = mediaSection.FindElements(By.TagName("a"));
                var mediaTags = mediaTagAndReferrence.Where(m => m.GetAttribute("href").Contains("/explore/tags")).ToList();
                var referrenceTags = mediaTagAndReferrence.Where(m => !m.GetAttribute("href").Contains("/explore/tags")).ToList();

                post.Caption = mediaText;

                foreach (var m in mediaTags)
                {
                    var tag = new Tag { URL = m.GetAttribute("href"), TagId = m.Text };
                    post.Tags.Add(tag);
                    Console.WriteLine("{0} {1}", m.GetAttribute("href"), m.Text);
                }

                post.Refs = new List<ReferenceProfile>();
                foreach (var r in referrenceTags)
                {
                    var @ref = new ReferenceProfile
                    {
                        URL = r.GetAttribute("href"),
                        RefId = r.Text
                    };
                    post.Refs.Add(@ref);
                    Console.WriteLine("{0} {1}", r.GetAttribute("href"), r.Text);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetImages(Post post)
        {
            Thread.Sleep(1000);
            var imgSection = Driver.FindElements(By.CssSelector("img")).Skip(1).ToList();

            foreach (var img in imgSection)
            {
                post.Images.Add(img.GetAttribute("src"));
                Console.WriteLine(img.GetAttribute("src"));
            }
        }
    }
}
