using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InstagramData.Core.Models;
using OpenQA.Selenium;

namespace InstagramData.Core
{
    public partial class InstagramController
    {
        public void RunV2(InstagramProfile ig)
        {
            //Driver.Navigate().GoToUrl(ig.URL);
            Driver.Url = ig.URL;
            Driver.Navigate();
            Thread.Sleep(1000);
            GetFollowerAndFollowingV2(ig);
            Thread.Sleep(1000);

        }

        private void GetFollowerAndFollowingV2(InstagramProfile igProfile)
        {
            try
            {
                /*
                 - private
                 + public && following <1000
                 */

                Thread.Sleep(2000);
                var selectedAllA = Driver.FindElements(By.CssSelector("a"));
                var followersTag = selectedAllA.Where(a => a.GetAttribute("href").Contains("/followers")).FirstOrDefault();

                //private account
                if (followersTag == null)
                {
                    return;
                }

                var followerCount = followersTag.FindElement(By.TagName("span")).GetAttribute("title");
                Console.WriteLine("Follower: {0}", followerCount);

                igProfile.FollowerCount = int.Parse(followerCount.Replace(",", ""));


                #region Follower
                //followersTag.Click();
                //Thread.Sleep(2000);

                ////.isgrP
                //((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, 100)");
                //Thread.Sleep(500);
                //((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, 200)");
                //Thread.Sleep(500);
                //((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, 300)");
                //Thread.Sleep(500);
                //((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, 400)");
                //Thread.Sleep(500);
                //((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, 500)");
                //Thread.Sleep(500);
                //((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, 600)");
                //Thread.Sleep(500);
                //((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, 700)");
                //Thread.Sleep(500);

                //long flagFollower = 0;
                //for (;;)
                //{
                //    ((IJavaScriptExecutor)Driver).ExecuteScript("document.querySelector('.isgrP').scrollTo(0, document.querySelector('.isgrP').scrollHeight )");
                //    Thread.Sleep(100);
                //    var currentHeight = (long)((IJavaScriptExecutor)Driver).ExecuteScript("return document.querySelector('.isgrP').scrollHeight;");

                //    if (flagFollower == currentHeight)
                //    {
                //        break;
                //    }
                //    else
                //    {
                //        flagFollower = currentHeight;
                //    }

                //    Thread.Sleep(1000);
                //}


                //var followersLinks = Driver.FindElements(By.CssSelector("li div a"));
                //var followers = followersLinks.Where(f => f.GetAttribute("title") != "").ToList();

                //var followersCount = followers.Count();
                //igProfile.FollowingCount = followersCount;
                //Console.WriteLine("Follower: {0}", followersCount);


                //foreach (var follower in followers)
                //{
                //    var igFollower = new Follower
                //    {
                //        Name = follower.GetAttribute("title"),
                //        URL = follower.GetAttribute("href")
                //    };

                //    igProfile.Followers.Add(igFollower);

                //    Console.WriteLine("{0} - {1}", follower.GetAttribute("title"), follower.GetAttribute("href"));
                //}

                //Thread.Sleep(2000);

                //var buttonCloseFollower = Driver.FindElement(By.CssSelector(".WaOAr > button"));
                //buttonCloseFollower.Click();

                //Thread.Sleep(2000);
                #endregion


                #region Following
                var followingTag = selectedAllA.Where(a => a.GetAttribute("href").Contains("/following")).FirstOrDefault();
                var followingCount = followingTag?.FindElement(By.TagName("span"))?.Text;

                if (followingCount == null) followingCount = "0";

                igProfile.FollowingCount = int.Parse(followingCount.Replace(",", ""));

                if (igProfile.FollowingCount > 1000 || igProfile.FollowingCount == 0) return;

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
                //igProfile.FollowingCount = followingsCount;
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
    }
}
