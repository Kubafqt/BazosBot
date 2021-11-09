using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net;
using System.Text.RegularExpressions;


namespace BazosBot
{
   class Download
   {
      public static List<string> htmlList = new List<string>();
      /// <summary>
      /// 
      /// </summary>
      /// <param name="url"></param>
      public static void DownloadAllFromCategory(string url) //from bazos section
      {
         int fullCount = 0;
         int actualNumber = 0;
         WebClient wc = new WebClient();
         do
         {
            string html = wc.DownloadString(url); //download html source from new url
            string[] lineSplit = html.Split("\n");
            fullCount = GetFullCount(lineSplit[189]);
            BazosOffers.GetOffersFromPage(html, url);
            PrepareNextPage(ref actualNumber, ref url);
         }
         while (actualNumber <= fullCount);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="actualNumber"></param>
      /// <param name="url"></param>
      private static void PrepareNextPage(ref int actualNumber, ref string url)
      {
         actualNumber += 20;
         if (Regex.IsMatch(url, @"/\d+"))
         {
            Regex.Replace(url, @"/\d+", actualNumber.ToString());
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
      /// <param name="line"></param>
      /// <returns></returns>
      private static int GetFullCount(string line)
      {
         int fullCount;
         string fullCountMatch = Regex.Match(line, @"z\s[\d ]*").ToString().Replace("z", string.Empty).Replace(" ", string.Empty);
         int.TryParse(fullCountMatch, out fullCount);
         return fullCount;
      }

   }
}
