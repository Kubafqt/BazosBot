﻿using System;
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
      public static string actualCategoryNameURL;
      public string nadpis { get; set; }
      public string popis { get; set; }
      public string datum { get; set; } //date after maybe
      public string url { get; set; }
      public string cena { get; set; }
      public int viewed { get; set; }
      public string lokace { get; set; }
      public string psc { get; set; }
      public BazosOffers(string nadpis, string popis, string datum, string url, string cena, int viewed, string lokace, string psc)
      {
         this.nadpis = nadpis;
         this.popis = popis;
         this.datum = datum;
         this.url = url;
         this.cena = cena;
         this.viewed = viewed;
         this.lokace = lokace;
         this.psc = psc;
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
      /// Main method to get all offers.
      /// </summary>
      public static void GetOffersFromPage(string html, string defUrl)
      {
         List<string> htmlSplit = html.Split("\n").ToList();
         htmlSplit.RemoveRange(0, 200);
         defUrl = defUrl.Substring(0, defUrl.LastIndexOf(".") + 3); //.cz, .sk
         int lineNumber = 0;
         foreach (string line in htmlSplit)
         {
            if(line.Contains("class=nadpis")) //nadpis, url, datum
            {      
               DictNameValue["url"] = GetOfferUrl(line, defUrl);
               DictNameValue["nadpis"] = GetNadpis(line);
               DictNameValue["datum"] = GetDate(line);
            }
            if (line.Contains("class=popis")) //popis
            {
               DictNameValue["popis"] = GetOfferPopis(htmlSplit, line, lineNumber);
            }
            if (line.Contains("class=\"inzeratycena\"")) //cena - kč, dohodou, nabídněte, v textu, ...
            {
               DictNameValue["cena"] = GetCena(line); 
            }
            if (line.Contains("class=\"inzeratylok\"")) //lokace, psč
            {
               int startIndex = line.IndexOf("\">") + 2;
               string[] subStrSplit = line.Substring(startIndex).Split("<");
               DictNameValue["lokace"] = subStrSplit[0];
               DictNameValue["psc"] = subStrSplit[1].Replace("br>", string.Empty);
            }
            if (line.Contains("class=\"inzeratyview\"")) //viewed count
            {
               DictNameValue["viewed"] = Regex.Match(line, @"\d+").ToString();
               OfferDictionaryToObject(); //last line - viewed count
            }
            lineNumber++;
         }
      }

      #region GetParametersFromHTML


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
                  break;
               }                     
            }
         }
         return string.Empty;
      }

      private static string GetNadpis(string line)
      {
         int startIndex = line.IndexOf("\">") + 2;
         int endIndex = line.IndexOf("</a>");
         return line.Substring(startIndex, endIndex - startIndex);
      }

      private static string GetOfferUrl(string line, string defUrl)
      {
         int urlStartIndex = line.IndexOf("<a href=") + 8;
         return defUrl + line.Substring(urlStartIndex, line.Length - urlStartIndex).Split('"')[1];
      }

      private static string GetDate(string line)
      {
         int dateStartIndex = line.LastIndexOf("[") + 1;
         int dateEndIndex = line.LastIndexOf("]</span>");
         return line.Substring(dateStartIndex, line.Length - dateStartIndex - (line.Length - dateEndIndex)).Replace(" ", string.Empty);
      }

      private static string GetCena(string line)
      {
         int startIndex = line.IndexOf("<b>") + 3;
         int endIndex = line.IndexOf("</b>");
         return line.Substring(startIndex, endIndex - startIndex).Trim().Replace(" ", string.Empty).Replace("Kč", string.Empty);
      }

      #endregion

      #region Add offer to list of objects
      private static void OfferDictionaryToObject()
      {
         ListBazosOffers.Add(new BazosOffers(DictNameValue["nadpis"], DictNameValue["popis"], DictNameValue["datum"], DictNameValue["url"], DictNameValue["cena"], int.Parse(DictNameValue["viewed"]), DictNameValue["lokace"], DictNameValue["psc"]));
         ResetStaticVariables();
      }

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