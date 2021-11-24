using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net;
using System.Windows.Forms;
using System.Threading;


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
      /// <summary>
      /// 
      /// </summary>
      /// <param name="url"></param>
      public static /*async Task*/ /*IEnumerable<int>*/ void DownloadAllFromCategory(string url, bool getOnlyNewOffers = false, bool autobot = false) //from bazos section
      {
         //await Task.Run(() =>
         //{
         int actualNumber = 0;
         downloadDone = false;
         isRunning = true;
         url = url[url.Length - 1] == '/' || url.Contains("?") ? url : url + "/";
         WebClient wc = new WebClient();
         do
         {
            string html = wc.DownloadString(url); //download html source from new url
            string[] lineSplit = html.Split("\n");
            int containerLineNumber = 0;
            fullCount = GetFullCount(lineSplit, ref containerLineNumber);
            if (BazosOffers.GetOffersFromPage(html, url, containerLineNumber, getOnlyNewOffers)) //download only new offers
            {
               downloadDone = true;
               DB_Access.InsertNewOffers(BazosOffers.actualCategoryURL);
               BazosOffers.ListBazosOffers.Clear(); //for showing in resultLbox
               BazosOffers.ListBazosOffers.AddRange(DB_Access.ListActualOffersInDB(BazosOffers.actualCategoryURL));
               isRunning = false;
               if (autobot)
               {
                  AutoBot.LastAutoBot.isRunning = false;
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
         if (autobot)
         {
            AutoBot.LastAutoBot.isRunning = false;
         }
         //});
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