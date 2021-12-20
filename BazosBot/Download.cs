using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net;
using System.Windows.Forms;
using System.Threading;
using RestSharp;
using System.Net.NetworkInformation;
//next - proxy server to webclient, DB save exception repair


namespace BazosBot
{
   class Download
   {
      public static bool getOnlyNewOffers = false;
      public static List<string> htmlList = new List<string>();
      public static double fullCount = 1;
      public static double count = 1;
      public static bool downloadDone = false;
      public static bool isRunning = false;
      public static bool waiting = false;
      static Random random = new Random();

      /// <summary>
      /// 
      /// </summary>
      /// <param name="url"></param>
      public static /*async Task*/ /*IEnumerable<int>*/ void DownloadAllFromCategory(string url, bool onlyNewOffers = false, bool botted = false) //from bazos section
      {
         //try
         //{
         //await Task.Run(() =>
         //{
            int downLimit = Settings.downLimit;
            int actualNumber = 0;
            downloadDone = false;
            isRunning = true;
            getOnlyNewOffers = onlyNewOffers;
            url = url[url.Length - 1] == '/' || url.Contains("?") ? url : url + "/";
            //WebClient wc = new WebClient();
            RestClient client = new RestClient();
            //string proxySite = Settings.proxyList[random.Next(Settings.proxyList.Length)];
            //client.Proxy = new WebProxy("169.57.1.84:8123"); //use proxy server - later not random, but choosed
            int downCount = 0;
            do
            {
               if (downCount >= downLimit && fullCount - count >= downLimit * 10) //500 offers - waiting to not overload the server
               {
                  downCount = 0;
                  waiting = true; //add report to timer
                  Thread.Sleep(random.Next(Settings.downLimitMinMax.X, Settings.downLimitMinMax.Y));
               }
               downCount++;
               waiting = false;
               RestRequest request = new RestRequest(url);
               string html = Encoding.Default.GetString(client.DownloadData(request));
               //string html = wc.DownloadString(url); //download html source from new url
               string[] lineSplit = html.Split("\n");
               int containerLineNumber = 0;
               fullCount = GetFullCount(lineSplit, ref containerLineNumber);
               if (BazosOffers.GetOffersFromPage(html, url, containerLineNumber, onlyNewOffers)) //download only new offers when condition met
               {
                  downloadDone = true;
                  DB_Access.InsertNewOffers(BazosOffers.actualCategoryURL, true);
                  BazosOffers.ListBazosOffers.Clear(); //for showing in resultLbox
                  BazosOffers.ListBazosOffers = onlyNewOffers ? DB_Access.newOffersList : DB_Access.ListActualOffersInDB(BazosOffers.actualCategoryURL);
                  isRunning = false;
                  if (botted)
                  {
                     AutoBot.LastBot.isRunning = false;
                  }
                  return;
                  //yield break; //means return
               }
               PrepareNextPage(ref actualNumber, ref url);
               count = actualNumber <= fullCount ? actualNumber : fullCount;
               //yield return actualNumber;
            }
            while (actualNumber <= fullCount);
            downloadDone = true;
            DB_Access.InsertNewOffers(BazosOffers.actualCategoryURL);
            isRunning = false;
            if (botted)
            {
               AutoBot.LastBot.isRunning = false;
            }
            //});
            //}
            //catch (Exception exeption)
            //{
            //   MessageBox.Show($"An error occured when trying to download offers from bazos - {exeption.GetType()}");
            //}
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public static bool IsConnectedToInternet(string url = "https://www.bazos.cz/")
      {
         Ping ping = new Ping();
         try
         {
            PingReply reply = ping.Send("1.1.1.1", 1000);
            if (reply.Status == IPStatus.Success)
            { return true; }
         }
         catch { }
         return false;
      }
      
      /// <summary>
      /// Prepare next bazos page.
      /// </summary>
      /// <param name="actualNumber"></param>
      /// <param name="url"></param>
      private static void PrepareNextPage(ref int actualNumber, ref string url)
      {
         actualNumber += 20;
         if (Regex.IsMatch(url, @"/\d+"))
         {
            url = Regex.Replace(url, @"/\d+", $"/{actualNumber}");
         }
         else
         {
            url = url.Contains("?") ? url.Replace("?", $"{actualNumber}/?") : url += $"{actualNumber}/";
         }
         Sleeping.MicroSleep(100, 150);
      }

      /// <summary>
      /// Get full count of offers from bazos section.
      /// </summary>
      /// <param name="lineSplit"></param>
      /// <returns>Count of offers in category on bazos.cz</returns>
      private static int GetFullCount(string[] lineSplit, ref int containerLineNumber)
      {
         int index = 0;
         int start = 120;
         string fullCountLine = string.Empty;
         List<string> searchLines = lineSplit.ToList().GetRange(start, 120);
         foreach (string line in searchLines)
         {
            if (line.Contains("maincontent"))
            {
               fullCountLine = searchLines[index + 4];
            }
            if (line.Contains("id=\"container_one\""))
            {
               containerLineNumber = start + index;
               break;
            }
            index++;
         }
         int fullCount;
         string fullCountMatch = Regex.Match(fullCountLine, @"z\s[\d ]*").ToString().Replace("z", string.Empty).Replace(" ", string.Empty);
         int.TryParse(fullCountMatch, out fullCount);
         return fullCount;
      }

   }
}