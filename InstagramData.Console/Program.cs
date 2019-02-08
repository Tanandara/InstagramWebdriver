using InstagramData.Core;
using InstagramData.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

                new InstagramProfile("https://www.instagram.com/aofpongsak/","aof"),
                //new InstagramProfile("https://www.instagram.com/bnk48/","BNK48"),
                //new InstagramProfile("https://www.instagram.com/cherprang.bnk48official/","cherprang"),
            };

                var userLogin = new User { Username = "", Password = "" };

                var instagramController = new InstagramController(userLogin, instagramProfiles,0);

                instagramController.Run();

                string aof = JsonConvert.SerializeObject(instagramProfiles[0]);
                //string bnk48 = JsonConvert.SerializeObject(instagramProfiles[0]);
                //string cherprang = JsonConvert.SerializeObject(instagramProfiles[1]);

                File.AppendAllText("aof.json", aof, Encoding.UTF8);
                //File.AppendAllText("cherprang.json", cherprang);

                //var csvExport = new CSVExport(instagramProfiles[0]);
                //csvExport.CreateGenaral();
                //csvExport.CreateFollowing();
                //csvExport.CreateMedia();

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine(ex.StackTrace);
            }

        }
    }
}
