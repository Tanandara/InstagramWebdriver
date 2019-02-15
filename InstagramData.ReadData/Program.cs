using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using InstagramData.Core.Models;
using InstagramData.Core;

namespace InstagramData.ReadData
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var amyJson = File.ReadAllText("amyun.u.json");

                var igAmy = JsonConvert.DeserializeObject<InstagramProfile>(amyJson);


                var userLogin = new User { Username = "", Password = "" };

                foreach (var following in igAmy.Followings)
                {
                    var fileName = string.Format("{0}\\{1}.json", igAmy.Name, following.Name);
                    if (File.Exists(fileName)) continue;

                    var instagramController = new InstagramController(userLogin, 0);

                    instagramController.Login();

                    var ig = new InstagramProfile(following.URL, following.Name);

                    instagramController.RunV2(ig);

                    var igJson = JsonConvert.SerializeObject(ig);

                    if (!Directory.Exists(igAmy.Name)) Directory.CreateDirectory(igAmy.Name);

                    File.WriteAllText(fileName, igJson, Encoding.UTF8);

                    instagramController.Close();

                }

                Console.WriteLine("Complete");
            }
            catch (Exception ex)
            {
                var errorMessage = string.Format("{0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace);

                Console.WriteLine(errorMessage);

                File.AppendAllText("Error.txt", string.Format("{0}{1}", Environment.NewLine, errorMessage));
            }
        }
    }
}
