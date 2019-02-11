using InstagramData.Core;
using InstagramData.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InstagramData.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var instagramProfiles = new List<InstagramProfile>() {

                new InstagramProfile("https://www.instagram.com/amyun.u/","amyun.u"),
            };

                var userLogin = new User { Username = "", Password = "" };

                var instagramController = new InstagramController(userLogin, instagramProfiles, 0);

                instagramController.Run();
                


                string amyJson = JsonConvert.SerializeObject(instagramProfiles[0]);

                var fileName = string.Format("{0}.json", instagramProfiles[0].Name);

                File.WriteAllText(fileName, amyJson, Encoding.UTF8);
                

                System.Console.WriteLine("Complete");

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine(ex.StackTrace);
            }

        }
    }
}
