using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BazosBot
{
   class BazosOffers
   {
      public static List<BazosOffers> ListBazosOffers = new List<BazosOffers>();
      public static string actualCategoryURL;
      public string nadpis { get; set; }
      public string popis { get; set; }
      public string datum { get; set; } //date after maybe
      public string url { get; set; }
      public string cena { get; set; }
      public int viewed { get; set; }
      public string lokace { get; set; }
      public string psc { get; set; }
      public string lastChecked { get; set; }
      public string changed { get; set; }
      public string endedDateTimeGetted { get; set; }
      public BazosOffers(string nadpis, string popis, string datum, string url, string cena, int viewed, string lokace, string psc, string lastChecked)
      {
         this.nadpis = nadpis;
         this.popis = popis;
         this.datum = datum;
         this.url = url;
         this.cena = cena;
         this.viewed = viewed;
         this.lokace = lokace;
         this.psc = psc;
         this.lastChecked = lastChecked;
         this.changed = string.Empty;
         this.endedDateTimeGetted = string.Empty;
         //ListBazosOffers.Add(this);
      }

      /// <summary>
      /// 
      /// </summary>
      private static Dictionary<string, string> DictNameValue = new Dictionary<string, string>()
      {
         { "nadpis" , "" },
         { "popis" , "" },
         { "datum" , "" },
         { "url" , "" },
         { "cena" , "" },
         { "viewed" , "" },
         { "lokace" , "" },
         { "psc" , "" }
      };

      /// <summary>
      /// Main method to get all offers from bazos section.
      /// </summary>
      public static bool GetOffersFromPage(string html, string defUrl, int containerLineNumber, bool getOnlyNewOffers)
      {
         List<string> htmlSplit = html.Split("\n").ToList();
         htmlSplit.RemoveRange(0, containerLineNumber);
         defUrl = defUrl.Substring(0, defUrl.LastIndexOf(".") + 3); //.cz, .sk
         int lineNumber = 0;
         bool urlNewDate = false;
         string nabCena = string.Empty;
         bool top = false;
         foreach (string line in htmlSplit)
         {
            if (line.Contains("class=nadpis")) //nadpis, url, datum
            {
               DictNameValue["url"] = GetOfferUrl(line, defUrl);
               DictNameValue["nadpis"] = GetNadpis(line, out top);
               DictNameValue["datum"] = GetDate(line, lineNumber, htmlSplit);
               if (getOnlyNewOffers && DB_Access.DBContainsUrl(DictNameValue["url"], actualCategoryURL, out urlNewDate, out nabCena) && !top)
               {
                  return true; //only new offers
               }
               //continue;
            }
            if (line.Contains("class=popis")) //popis
            {
               DictNameValue["popis"] = GetOfferPopis(htmlSplit, line, lineNumber);
               //continue;
            }
            if (line.Contains("class=\"inzeratycena\"")) //cena - kč, dohodou, nabídněte, v textu, ...
            {
               DictNameValue["cena"] = GetCena(line);
               if (urlNewDate) //not get new offer is same price on only new date on existing url
               {
                  urlNewDate = false;
                  if (nabCena == DictNameValue["cena"]) //new date, but same price - not new offer
                  {
                     nabCena = string.Empty;
                     continue;
                  }
                  nabCena = string.Empty;
               }
               //continue;
            }
            if (line.Contains("class=\"inzeratylok\"")) //lokace, psč

            {
               int startIndex = line.IndexOf("\">") + 2;
               string[] subStrSplit = line.Substring(startIndex).Split("<");
               DictNameValue["lokace"] = subStrSplit[0];
               DictNameValue["psc"] = subStrSplit[1].Replace("br>", string.Empty);
               //continue;
            }
            if (line.Contains("class=\"inzeratyview\"")) //viewed count
            {
               DictNameValue["viewed"] = Regex.Match(line, @"\d+").ToString();
               if (getOnlyNewOffers && top)
               {
                  ResetStaticVariables();
                  continue;
               }
               OfferDictionaryToObjectList(); //last line - viewed count
               ResetStaticVariables();
            }
            lineNumber++;
         }
         return false;
      }

      #region GetParametersFromHTML

      /// <summary>
      /// 
      /// </summary>
      /// <param name="htmlSplit"></param>
      /// <param name="line"></param>
      /// <param name="lineNumber"></param>
      /// <returns></returns>
      private static string GetOfferPopis(List<string> htmlSplit, string line, int lineNumber)
      {
         int startIndex = line.IndexOf("=popis>") + 7;
         if (line.Contains("</div>"))
         {
            return line.Substring(startIndex, line.Length - startIndex).Split("<")[0]; //asumme that "<" char is not in text
         }
         else
         {
            string popis = line.Substring(startIndex, line.Length - startIndex);
            for (int i = lineNumber + 1; ; i++)
            {
               if (!htmlSplit[i].Contains("</div>"))
               {
                  popis += "\n" + htmlSplit[i].Trim();
               }
               else
               {
                  popis += "\n" + htmlSplit[i].Split("<")[0];
                  return popis;
               }
            }
         }
         //return string.Empty;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="line"></param>
      /// <returns></returns>
      private static string GetNadpis(string line, out bool top)
      {
         int startIndex = line.IndexOf("\">") + 2;
         int endIndex = line.Contains("</a>") ? line.IndexOf("</a>") : line.Length; //GetUrlClosingLine(index, htmlSplit);
         top = line.Contains("class=\"ztop\"") ? true : false;
         return line.Substring(startIndex, endIndex - startIndex);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="line"></param>
      /// <param name="defUrl"></param>
      /// <returns></returns>
      private static string GetOfferUrl(string line, string defUrl)
      {
         int urlStartIndex = line.IndexOf("<a href=") + 8;
         return defUrl + line.Substring(urlStartIndex, line.Length - urlStartIndex).Split('"')[1];
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="line"></param>
      /// <param name="index"></param>
      /// <param name="htmlSplit"></param>
      /// <returns></returns>
      private static string GetDate(string line, int index, List<string> htmlSplit)
      {
         if (line.Contains("</a>"))
         {
            int dateStartIndex = line.LastIndexOf("[") + 1;
            int dateEndIndex = line.LastIndexOf("]</span>");
            return line.Substring(dateStartIndex, line.Length - dateStartIndex - (line.Length - dateEndIndex)).Replace(" ", string.Empty);
         }
         else
         {
            line = GetUrlClosingLine(index, htmlSplit);
            int dateStartIndex = line.LastIndexOf("[") + 1;
            int dateEndIndex = line.LastIndexOf("]</span>");
            return line.Substring(dateStartIndex, line.Length - dateStartIndex - (line.Length - dateEndIndex)).Replace(" ", string.Empty);
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="index"></param>
      /// <param name="htmlSplit"></param>
      /// <returns></returns>
      private static string GetUrlClosingLine(int index, List<string> htmlSplit)
      {
         for (int i = index; i < index + 10; i++)
         {
            string line = htmlSplit[i];
            if (line.Contains("</a>"))
            {
               return line;
            }
         }
         return string.Empty;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="line"></param>
      /// <returns></returns>
      private static string GetCena(string line)
      {
         int startIndex = line.IndexOf("<b>") + 3;
         int endIndex = line.IndexOf("</b>");
         return line.Substring(startIndex, endIndex - startIndex).Trim().Replace(" ", string.Empty).Replace("Kč", string.Empty);
      }

      #endregion

      #region Add offer to list of objects
      /// <summary>
      /// 
      /// </summary>
      private static void OfferDictionaryToObjectList()
      {
         foreach (var kvp in DictNameValue)
         {
            DictNameValue[kvp.Key] = TextAdjust.PrepareToCommand(kvp.Value);
         }
         ListBazosOffers.Add(new BazosOffers(DictNameValue["nadpis"], DictNameValue["popis"], DictNameValue["datum"], DictNameValue["url"], DictNameValue["cena"], int.Parse(DictNameValue["viewed"]), DictNameValue["lokace"], DictNameValue["psc"], DateTime.Now.ToString()));
         //ResetStaticVariables();
      }

      /// <summary>
      /// 
      /// </summary>
      private static void ResetStaticVariables()
      {
         foreach (string key in DictNameValue.Keys)
         {
            DictNameValue[key] = string.Empty;
         }
      }

      #endregion

   }
}