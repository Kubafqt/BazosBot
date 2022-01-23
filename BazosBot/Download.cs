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
      public static double count = 1;
      public static double fullCount = 1;
      public static bool downloadDone = false;
      public static bool isRunning = false;
      public static bool waiting = false;
      public static bool stopped = false;
      static Random random = new Random();

      /// <summary>
      /// Ping to public DNS server to test internet connectivity.
      /// </summary>
      /// <returns></returns>
      public static bool IsConnectedToInternet()
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
      /// Download all pages in category.
      /// </summary>
      /// <param name="url"></param>
      /// <param name="onlyNewOffers"></param>
      /// <param name="botted"></param>
      public static void DownloadAllFromCategory(string url, bool onlyNewOffers = false, bool botted = false) //from bazos section
      {
         int downLimit = Settings.downLimit;
         int actualNumber = 0;
         downloadDone = false;
         isRunning = true;
         getOnlyNewOffers = onlyNewOffers;
         url = url[url.Length - 1] == '/' || url.Contains("?") ? url : url + "/";
         BazosOffers.actualCategoryURL = url;
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
            string[] lineSplit = html.Split("\n");
            int containerLineNumber = 0;
            fullCount = GetFullCount(lineSplit, ref containerLineNumber);
            if (BazosOffers.GetOffersFromPage(html, url, containerLineNumber, onlyNewOffers)) //download only new offers when condition met
            {
               DownloadFinished(botted, onlyNewOffers);
               return;
            }
            PrepareNextPage(ref actualNumber, ref url);
            count = actualNumber <= fullCount ? actualNumber : fullCount;
         }
         while (actualNumber <= fullCount && !stopped);
         DownloadFinished(botted);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="urlForFullContent"></param>
      public static void DownloadFullContent(List<string> urlsForFullContent)
      {
         int downLimit = Settings.downLimit;
         int actualNumber = 0;
         downloadDone = false;
         isRunning = true;
         getOnlyNewOffers = onlyNewOffers;
         url = url[url.Length - 1] == '/' || url.Contains("?") ? url : url + "/";
         BazosOffers.actualCategoryURL = url;
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
            string[] lineSplit = html.Split("\n");
            int containerLineNumber = 0;
            fullCount = GetFullCount(lineSplit, ref containerLineNumber);
            if (BazosOffers.GetOffersFromPage(html, url, containerLineNumber, onlyNewOffers)) //download only new offers when condition met
            {
               DownloadFinished(botted, onlyNewOffers);
               return;
            }
            PrepareNextPage(ref actualNumber, ref url);
            count = actualNumber <= fullCount ? actualNumber : fullCount;
         }
         while (actualNumber <= fullCount && !stopped);
         DownloadFinished(botted);


      }

      /// <summary>
      /// When download finished.
      /// </summary>
      /// <param name="botted"></param>
      /// <param name="onlyNewOffers"></param>
      private static void DownloadFinished(bool botted, bool onlyNewOffers = false)
      {
         downloadDone = true;
         DB_Access.InsertNewOffers(BazosOffers.actualCategoryURL, onlyNewOffers);
         isRunning = false;
         if (botted)
         {
            AutoBot.LastBot.isRunning = false;
            AutoBot.LastBot.stoppedRunning = true;
         }
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