using System.Collections.ObjectModel;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using steadyforum.Server.Model;

namespace steadyforum.Server.Service
{
    public class NewsCrawler : INewsCrawler
    {
        public int searchTelegramNews()
        {
            /*pre set*/

            Collection<string> keyWord = new Collection<string>() { "hack group" };
            Collection<string> newsTelegrammRecource = new Collection<string>() { "https://www.securitylab.ru/", "https://t.me/s/SecLabNews" };
            string telegrammToken = "";
            int newsCounter = 0;

            foreach (var newsLink in newsTelegrammRecource)
            {

                /*request*/

                /*sample*/

                /*generade background gif*/

                /*insert db used News DTO*/

                new News
                {

                };
            }

            return newsCounter;
        }
    }
}