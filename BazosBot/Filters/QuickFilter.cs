using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazosBot
{
   class QuickFilter
   {
      public static List<QuickFilter> QuickFilterList = new List<QuickFilter>();
      public string nadpis { get; set; }
      public string popis { get; set; }
      public int maxCena { get; set; }
      public bool fullNadpisName { get; set; }
      public QuickFilter(string nadpis, string popis, int maxCena, bool fullNadpisName)
      {
         this.nadpis = nadpis;
         this.popis = popis;
         this.maxCena = maxCena;
         this.fullNadpisName = fullNadpisName;
         QuickFilterList.Add(this);
      }



   }
}
