using InstagramData.Core.Models;
using InstagramData.MergeJSON.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramData.MergeJSON
{
    class Program
    {
        static void Main(string[] args)
        {

            var igMergeProfile = new InstagramMergeProfile
            { URL = "https://www.instagram.com/amyun.u/", Name = "amyun.u" };

            var jsonFiles = Directory.EnumerateFiles(igMergeProfile.Name,"*.json");

            foreach (string file in jsonFiles)
            {
                string json = File.ReadAllText(file);

                var instagramFollowing = JsonConvert.DeserializeObject<InstagramProfile>(json);

                igMergeProfile.Followings.Add(instagramFollowing);

                Console.WriteLine("{0}", instagramFollowing.Name);
            }

            var fileName = string.Format("{0}_Followings.json", igMergeProfile.Name);

            var content = JsonConvert.SerializeObject(igMergeProfile);

            File.WriteAllText(fileName, content);

            Console.WriteLine("Complete");

            Console.ReadLine();
        }
    }
}
