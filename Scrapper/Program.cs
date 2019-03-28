using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Scrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            GethtmlAsync();
            Console.ReadLine();
        }

        private static async void GethtmlAsync()
        {
            var url = "https://www.hltv.org/matches/2332126/furia-vs-singularity-esea-mdl-season-30-north-america";

            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var Bet365sHTML = htmlDocument.DocumentNode.Descendants("tr")
                .Where(node => node.GetAttributeValue("class", "")
                    .Equals(" geoprovider_bet365 betting_provider")).ToList();

            var BettingListItems = Bet365sHTML[0].Descendants("a")
                .Where(node => node.GetAttributeValue("target", "")
                    .Contains("_blank")).ToList();



            foreach (var BettingListItem in BettingListItems)
            {
                Console.WriteLine(BettingListItem.DescendantNodes("td")
                    .Where(node => node.GetAttributeValue("target", "")
                        .Equals("_blank")).FirstOrDefault().InnerHtml
                );


            }

            //var BettingList = Bet365sHTML[0].Descendants()      

            //Console.WriteLine();
        }
    }
}
